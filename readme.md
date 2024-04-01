# Third Way Web Development Labs/Companion Project

## Introduction

HTML, while powerful, is currently insufficient for building modern and dynamic web applications. As currently specified, only large-grain (full page) interactions are supported. AJAX, popularized with the introduced of [Google Maps](https://maps.google.com) in 2005 demonstrated an approach to leveraging asynchronous requests and DOM manipulation in JavaScript to achieve more fine-grained control over page interactions which introduced the Web 2.0 paradigm. Achieving this is pure JavaScript has historically been challenging. The first challenge was inconsistent implementation across browsers.

### The Ajax Library Era

In 2006, a number of JavaScript libraries emerged including [jQuery](https://jquery.com/), [YUI](https://yui.github.io/yui2/docs/yui_2.9.0_full/) and [ext.js](https://en.wikipedia.org/wiki/Ext_JS). These libraries introduced a browser agnostic API for scripting dynamic interactions and partial page updates. The result was effective but could become unwieldy for large, complex applications. Imperative DOM manipulation introduced a lot of code to write and manage, and we began to look at approaches to reduce boilerplate, including introducing client-side templating libraries. While some lightweight AJAX libraries remain popular, their APIs have typically seen several breaking changes over time and large amounts of code can be challenging to maintain and test.

### The First Reactive Frameworks

Even with client-side templating, a lot of boilerplate was still necessary. Frameworks began to emerge to abstract and automate DOM manipulations by binding templates to underlying data and observing/reacting to state changes. Although a lot of boilerplate was eliminated, these approaches still required a lot of code to be written and maintained. A lot of these first-generation frameworks moved the then-dominant MVC/MVVM paradigm from the sever to the client which also promised some structure and separation of concerns of client-side code. That said, early reactive frameworks were often not especially opinionated, which resulted in increasingly complex and byzantine client-side applications. This code was also irrevocably coupled to framework versions; as frameworks evolved and addressed problems in earlier designs the code had to modified to maintain compatibility with the current version of the frameworks. The overhead and burden of maintenance became increasingly higher.

### Component-based Frameworks

The next wave of frameworks introduced an alternative approach to structuring and organizing codebases. Applications could be composed of components of varying granularity. This introduced improved modularity, structure, and maintainability of code bases. As more and more application logic and behavior could be built and maintained on the client, Single-Page Applications (SPAs) became increasingly common; component-based frameworks simplified this transition.

The frameworks continued to evolve considerably and, in the process, often introduced breaking changes which further increased the overhead and burden of maintenance. Early versions of these frameworks ignored the expectations of hypermedia which introduced various issues including problems with deep linking, state management, SEO, bookmarking, and progressive enhancement/graceful degradation. As the complexity of these modern frameworks grew, deep specialization became increasingly necessary to work effectively with them. Coupling between client and server is now increasingly common and much application logic has moved to the less trustworthy client environment which further adds complication and complexity.

The latest generation of client-side frameworks have become the de-facto best practice for modern web development. Despite the challenges of maintenance, complexity, security, and managing regular breaking changes; this approach has been undeniably useful and has enabled much innovation in the web development space.

### HTMX - Hypermedia 2.0

HTMX presents an alternative approach to building modern and dynamic web applications. Hypermedia As The Engine Of Application State (HATEOAS) is a powerful and flexible paradigm that has powered over three-decades of web evolution. The key shortcoming of HTML as a hypermedia is the inflexibility of granularity of interactions. Rather than build a custom API to code against that will inevitably change over time, HTMX seeks to extend HTML as a hypermedia. Developers of HTMX-powered web applications simply write standards-compliant HTML and CSS. Unlike first-gen ajax libraries and modern frameworks; rarely--if ever--will a developer write JavaScript code to interact with HTMX directly. Instead, HTMX acts as a HTML polyfil to enhance the native capabilities of HTML. Using HTMX simply requires adding additional properties to HTML tags to declaratively improve behavior. This approach supports both progressive enhancement and graceful degradation. The use of declarative tag properties rather than imperative coding also introduces a powerful abstraction allowing HTMX to continue to evolve in meaningful ways without breaking existing implementations in the same way that HTML itself has evolved from 1.0 - 5, never requiring [old pages](https://info.cern.ch/) to be rewritten as new capabilities are introduced.

To borrow a summary from the [official HTMX docs](https://htmx.org/docs/):

HTMX extends and generalizes the core idea of HTML as a hypertext, opening up many more possibilities directly within the language:

- Now any element, not just anchors and forms, can issue an HTTP request
- Now any event, not just clicks or form submissions, can trigger requests
- Now any HTTP verb, not just GET and POST, can be used
- Now any element, not just the entire window, can be the target for update by the request

Note that when you are using htmx, on the server side you typically respond with HTML, not JSON. This keeps you firmly within the [original web programming model](https://www.ics.uci.edu/~fielding/pubs/dissertation/rest_arch_style.htm), using [Hypertext As The Engine Of Application State](https://en.wikipedia.org/wiki/HATEOAS) without even needing to really understand that concept.

It's worth mentioning that, if you prefer, you can use the data- prefix when using htmx: `<a data-hx-post="/click">Click Me!</a>` (which  will keep your HTML completely valid, only using properties defined in the spec. This is optional, for the purposes of this workshop, we won't be following this convention).

This project provides a playground and an example of how a Web 1.0 application can be modernized to provide the modern, dynamic user experience we have come to expect without adding excessive code that must be organized, maintained, and managed. Applications written using HTMX don't suffer from the complexity, tech debt, and ephemerality of framework-based web apps. This is not to say HTMX applications are superior, simply that a meaningful alternative to framework-centric web apps exists. Web 1.0, framework-centric, and HTMX apps all have their benefits and trade-offs.

#### When to Use HTMX

In short, wherever it makes sense. Some examples might me:

- To modernize a legacy server-side MVC app without a costly and complex rewrite
- An application that must be built and maintained by teams who don't possess significant expertise in any of the mainstream frameworks
- As a less complex and more maintainable approach to building a new web app

## Our Web 1.0 Application - an RSS Reader

Among the interesting ideas of the web that have fallen by the wayside, decentralized publishing may be added to the list. Similar to how frameworks abandoned hypermedia, relegating it to a mere UI definition language (UIML?), and lost many of its benefits in the process; publishing to the web has largely become centralized. One major consequence of this is we ceded control of our content feeds. On social media, for example, I may follow several creators however that relationship is controlled by the publisher. My feed--what I see--is typically algorithmically generated, optimizing for ad revenue and engagement rather than simply providing a single feed of the content from the publishers I want to follow. Like frameworks, it's not all bad; I've discovered much in the process. That said, syndication standards have long allowed us to curate our own feeds which we may follow without manipulation. Really Simple Syndication (RSS) is among the first of many standards to support this.

Our Web 1.0 Application is a very simplistic RSS reader capable of subscribing to the published feeds of several creators. It is a cross-platform MVC application built using C# and .net 8.0. Like most modern, sever-side web development frameworks, .net core web frameworks support many modern web development tools and concepts including templating, validation, pages, partial views, components, APIs, and security controls. For the sake of simplicity and to keep this workshop as backend framework agnostic as possible, I have tried to keep the implementation simple and use few framework-specific features. However, I want to point out that they do exist and most of what is demonstrated in this lab can be applied to the backend language and framework of your choice.

### Running this application

To run this application locally you'll need:

- A code editor (such as [Visual Studio Code](https://code.visualstudio.com/Download))
- The [.Net 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- A container runtime such as [Docker](https://www.docker.com/products/docker-desktop/) or [Rancher](https://rancherdesktop.io/)

Alternatively, the repo can be developed and debugged entirely online using [GitPod.io](https://gitpod.io/)

## Lab 1 - Getting Set up (10 mins)

Although this can be run entirely locally, if you're not already set up for .net development, I recommend using gitpod.

1. Head to [GitPod.io](https://gitpod.io/) and log in using your GitHub, GitLab, or bitbucket credentials.
1. Click "New Workspace"
1. Click "Select a Repository" and enter the repo address (`https://github.com/carducci/ThirdWayWorkshop`) and create the workspace.

At this point, an in-browser instance of visual studio code will be launched and configured for this development environment.

First we'll restore any packages and dependencies. In the terminal, type the following (remember the Linux filesystem is case sensitive):

```bash
gitpod /workspace/ThirdWayWorkshop (main) $ dotnet restore
```

When that completes, `cd` into the web application directory

```bash
gitpod /workspace/ThirdWayWorkshop (main) $ cd src/ThirdWay.Web
```

From here, you should be able to run the project using the `dotnet run` command

```bash
gitpod /workspace/ThirdWayWorkshop (main) $ dotnet run
```

At this point, the project will be built and the container started. Gitpod will prompt you to "Open Preview" which will embed a browser in your environment. Alternatively the preview can be opened in a new window.

I recommend creating a new window for the application preview and keeping this window open for the duration of this workshop. The URL will remain the same throughout your gitpod session and the page refreshed anytime you restart the application from the VS Code terminal.

The reader is not yet subscribed to any feeds, go ahead and add one.

1. Click "Manage Feeds"
1. In the input box, enter `https://sufficiently-advanced.technology/feed.xml`
1. Click "+ Add Feed"

Navigating to "All" on the left menu will show the first page of posts retrieved from the feed. Individual posts can be viewed, favorited, and marked as read/unread. Posts can be searched using the top search bar, and all feeds can be refreshed using the refresh icon on the top of the page. Finally, feeds can be added or removed from the "Manage Feeds" link.

Although this application is simple, small, and thus quite performant, to illustrate the pain of the Web 1.0 experience, a 1.5 second delay has been added to every request.

Explore the application to ensure your development environment is working correctly and to get a feel for this application.

### Application Structure

Within the ThirdWay.Web project, we will mainly be working with controllers and views which can be found in the `Controllers` and `Views` folders respectively.

`FeedController.cs` defines all endpoints that begin with `/Feed` and `PostController.cs` defines all endpoints that begin with `/Post`.

A single view defines the UI for all Post lists, `/Views/Post/List.cshtml`. Additionally, `Post.cshtml` defines the UI for viewing a single post.

Feed only offers one view, the list of subscribed feeds, which is defined by `index.cshtml`

Additionally the main layout for the application is defined in `/Views/Shared/_layout.cshtml` which defines our default template.

### Razor Template Syntax

.Net MVC uses the Razor Templating system which provides a number of features and capabilities. In general, the views are just HTML however code/language elements can be introduced by prefixing code with `@`. Code blocks may be opened where necessary but, in most cases, model properties are inserted simply by prefixing the variable with `@` e.g.

```html
 ...
 <h1 class="post title">@Model.Title</h1>
 ...
```

Razor will handle sanitizing and encoding unless `@Html.Raw()` is used, in which case the content will be inserted into the template without encoding or sanitization. Use `@Html.Raw()` with care.

## Lab 2 - Installing HTMX (5 minutes)

HTMX is a single lightweight (~14kb gzipped) and dependency-free library. In most cases it will be installed by adding a `<script>` tag to your html template, however in can be installed using JavaScript package managers (e.g. `npm install htmx.org`). Since a build step is not necessary for this project, we can simply reference the library from a CDN or download it to `/wwwroot/lib`.

1. Open `/Views/Shared/_Layout.cshtml`
2. Add the following `<script>` tag to the `<head>`

```html
<script src="https://unpkg.com/htmx.org@1.9.11" integrity="sha384-0gxUXCCR8yv9FM2b+U3FDbsKthCI66oH5IA9fHppQq9DDMHuMauqq1ZHBpJxQ0J0" crossorigin="anonymous"></script>
```

N.B. When referencing external files such as this, always define `crossorigin="anonymous"`

That's it! The entire application is now using Fielding's code-on-demand constraint to make HTML a significantly more powerful hypermedia.

Verify HTMX has loaded correctly by running the application and typing `htmx.version` in the console.

## Lab 3 - Introducing hx-boost (10 mins)

One advantage Single-Page Applications have over Web 1.0 applications is their ability to perform page navigation without necessarily throwing away the entire DOM, CSS rules, and JavaScript files. A full-page change often requires retrieving these resources again (admittedly, often from cache) and parsing/executing them during every navigation event.

HTMX offers a simple mechanism to begin AJAX-ifying an application in the form of "boosted" hypermedia controls. The `hx-boost` attribute, when added to an anchor or form, quickly transforms these into more fine-grained AJAX controls. Instead of replacing the whole page, HTMX will simply replace the contents of the `<body>` tag which improves user-perceived performance since the browser doesn't need to parse and interpret scripts and stylesheets in the `<head>` over and over again.

### 3.1 Boosting Posts

Navigation from the post list, to an individual post involves clicking on a hyperlink. You'll find the specific hyperlink on line 29 of `/Views/Post/List.cshtml`

```html
...
 <div class="read-more">
  <a href="/Post/Id/@post.Id" class="rss-btn gold-bg">Read More</a>
 </div>
...
```

We're going to transform this into a "boosted" link by adding this property to the anchor tag: `hx-boost="true"`.

```html
...
 <div class="read-more">
  <a href="/Post/Id/@post.Id" class="rss-btn gold-bg">Read More</a>
 </div>
...
```

becomes:

```html
...
 <div class="read-more">
  <a href="/Post/Id/@post.Id" 
   class="rss-btn gold-bg"
   hx-boost="true">Read More</a>
 </div>
...
```

Rebuild the application and verify this is now an AJAX operation by opening a post while the network inspector is open in the browser developer tools. It should now be an XHR request.

### 3.2 More Boosting and Inheritance

In many cases, this is behavior we might wish to deploy across our application. While this could be accomplished by adding the `hx-boost="true"` property to every hyperlink and form, HTMX also supports attribute inheritance, what the authors refer to as "cascading HTMX attributes" inspired by "cascading style sheets."

Let's apply boosting globally by adding this property to the `<body>` tag in `Views/Shared/_layout.cshtml` on line 12.

```html
<body>
```

becomes

```html
<body hx-boost="true">
```

Save and run the application to verify that all local interactions are boosted. You'll likely note as you navigate around that, despite navigation within the app being entirely ajax, HTMX is maintaining the correct location within the browser. Back and Forward buttons will work as expected, as will bookmarks. HTMX provides sensible defaults for when the location bar should be updated, but you can override these defaults if/when it makes sense. We'll explore this in upcoming labs.

Notably HTMX won't boost external links, and cases where you might want to override this behavior on a specific link can be accomplished by applying the `hx-boost="false"` attribute. E.g.

```html
...
    <a href="/files/report.pdf" hx-boost="false">Download PDF</a>
...
```

Save and verify that all internal forms and links are now boosted.

## Lab 4 - Security Considerations (5 minutes)

Navigating our reader app with the network inspector open, you'll notice that HTMX is not only dynamically replacing content in the body tag, it is also loading additional resources and parsing html and applying behaviors to inserted elements. Like most server-side frameworks, the .net core MVC framework is automatically sanitizing anything merged into a template with one glaring exception, the post body. The atom feed gives us the post as HTML which we are inserting into the page in its raw form.

Of course, we are sanitizing this, as you can see in `Views/Post/Post.cshtml` on lines 33-38:

```html
...
 <div class="text-white post-content">
  @{
   var sanitizer = new HtmlSanitizer();
   @Html.Raw(sanitizer.Sanitize(Model.Body))
  }
 </div>
...
```

However our sanitizer doesn't know about HTMX attributes out of the box and, if left unchecked, we introduce a new vector of attack. A bad actor might add HTMX attributes to a feed which would be interpreted by our application.

To prevent this, we will add a new HTMX attribute to our post content container div, the `hx-disable="true"` attribute. Like many other HTMX attributes, this attribute is inherited to all child elements and, unlike the `hx-boost` attribute, cannot be overridden in a child element.

The block of code beginning on line 33 becomes:

```html
...
 <div class="text-white post-content" hx-disable="true">
  @{
   var sanitizer = new HtmlSanitizer();
   @Html.Raw(sanitizer.Sanitize(Model.Body))
  }
 </div>
...
```

We are now using this tool securely and responsibly as this is the only place in the application where unsanitized output is presented using `@Html.Raw()`.

## Lab 5 - Beyond GET and POST and Enhanced UX (15 mins)

The Uniform Interface constraint of REST focuses on a handful of primary components:

- The Resource Abstraction
- Stable URIs as Resource Identifiers
- Semantically constrained interactions driven by passing representations of resources back and forth

In general, this application follows a consistent pattern for resource identifiers as follows:

`/{collection/class}/{collection sub-partition}/{sub-partition identifier}`

We have `/Post` which will retrieve a collection of posts.
We have `/Post/Id/1` which will retrieve an individual post instance.

Likewise we have `/Feed` which will retrieve a collection of feeds.

However, the current HTML standard only supports `GET` and `POST` operations within the built-in hypermedia controls, which means we have to deviate from the uniform interface somewhat and introduce an awkward endpoint `/Feed/Id/1/Delete` which is accessed with a POST operation. We are no longer so constrained as HTMX allows us to perform a semantically consistent `DELETE` operation.

### 5.1 Updating our Controller

Let's create a new endpoint in `Controllers/Feed.cs` to perform a `DELETE` operation on a named feed resource.

On a new line after line 67 in `Controllers/Feed.cs` add the following:

```csharp
 [HttpDelete("/Feed/Id/{id}")]
 public async Task<IActionResult> DeleteFeed(int id)
 {
  try
  {
   await _feedService.DeleteFeed(id);
   await Bottleneck();
  }
  catch(Exception){
   //Something went wrong here. Most likely a 404
   //A delete is supposed to be idempotent so we will ignore this
  }
  // Return a 204 No Content response
  return NoContent();
 }
```

Let's also annotate the legacy Web 1.0 POST endpoint on line 59 as deprecated since it remains for compatibility but is no longer the preferred and canonical endpoint.

```csharp
...
 [HttpPost("/Feed/Id/{id}/DeleteFeed")]
 public async Task<IActionResult> Delete(int id)
 {
...
```

becomes:

```csharp
...
 [HttpPost("/Feed/Id/{id}/DeleteFeed")]
 [Obsolete("This endpoint exists for compatibility/graceful degration only. A proper DELETE request on the resources is preferred")]
 public async Task<IActionResult> Delete(int id)
 {
...
```

### 5.2 Updating our View

Next we will enhance our feed list to properly delete the resource.

Open `Views/Feed/index.cshtml` and navigate to line 37 to the existing Web 1.0 form implementation. It should look like this:

```html
...
 <form method="post" action="/Feed/Id/@feed.Id/DeleteFeed" style="display: inline;">
  <button type="submit" class="btn btn-link" aria-label="Delete Feed" title="Delete Feed">
   <i class="fa-solid fa-trash gold-txt"></i>
  </button>
 </form>
...
```

Although the button is part of a (boosted) form, we can progressively enhance the behavior of this button by adding the `hx-delete` attribute which, when HTMX is loaded, will override the default behavior and issue a proper `DELETE` request.

Our button, which begins on line 37:

```html
...
 <button type="submit" class="btn btn-link" aria-label="Delete Feed" title="Delete Feed">
  <i class="fa-solid fa-trash gold-txt"></i>
 </button>
...
```

becomes

```html
...
 <button type="submit" 
   class="btn btn-link" 
   aria-label="Delete Feed" 
   title="Delete Feed"
   hx-delete="/Feed/Id/@feed.Id">
  <i class="fa-solid fa-trash gold-txt"></i>
 </button>
...
```

In this case, the button itself becomes an independent, standalone hypermedia control. When HTMX is running the form itself becomes irrelevant. This is a key capability of HTMX, to enable any element to become a hypermedia control and fully participate in a hypermedia driven application.

This implementation will gracefully degrade, however if this is not necessary, the wrapping form and the obsolete method may simply be removed.

### 5.3 Adding Client-Side Behavior

The obsolete Delete method will return a new representation of the `/Feed` collection, but a `DELETE` method does not return a representation of any resource, just a `204` response indicating the request was successful but there is no content to return.

We won't see a change in state of the `/Feed` resource until we make a new request by refreshing the page or navigating to the "Feeds" view from the left-nav.

While our hypermedia interactions are still fairly large-grain, we can get much more fine-grained using the `hx-target` attribute. This defines the scope of the resource we are interacting with and permits very fine-grained interaction. In this case, the `/Feed` resource is a composite of all the individual Feed resources. The target resource is the parent div of the entry in the list, which we can identify as the closest div with a class of "row".

The `hx-target` attribute will swap the content of the identified element with whatever the response is. In our case no content is returned so the entire row will be removed.

N.B. As of the current versions of HTMX, a [204 response is interpreted as requiring no action](https://github.com/bigskysoftware/htmx/issues/1130) (since there is no response body). Only successful responses which are defined as having a body will trigger a swap operation. Since we are aiming to provide an idiomatic uniform interface for our application, I have modified the configuration of HTMX to instruct HTMX to swap on 204 responses. You can see this modification in `wwwroot/js/site.js` and read more about this [here](https://htmx.org/docs/#modifying_swapping_behavior_with_events).

By default, `hx-target` will swap the inner html of the element, but here we can remove the entire row div so we want to replace the outer html. We can further extend the swap operation with a CSS animation to fade the row out.

Our button:

```html
...
 <button type="submit" 
   class="btn btn-link" 
   aria-label="Delete Feed" 
   title="Delete Feed"
   hx-delete="/Feed/Id/@feed.Id">
  <i class="fa-solid fa-trash gold-txt"></i>
 </button>
...
```

Becomes:

```html
...
 <button type="submit" 
   class="btn btn-link" 
   aria-label="Delete Feed" 
   title="Delete Feed"
   hx-delete="/Feed/Id/@feed.Id"
   hx-target="closest div.feedEntry"
   hx-swap="outerHTML swap:1s">
  <i class="fa-solid fa-trash gold-txt"></i>
 </button>
...
```

HTMX will automatically apply an `hx-swapping` class during the swap operation (which we've added a delay to to support a fadeout transition). In the application CSS, this rule is defined as follows:

```css
...
 div.feedEntry.htmx-swapping{
  opacity: 0;
  transition: opacity 1s ease-out;
 }
...
```

Since a `DELETE` operation has destructive side-effects, we probably want to guard against accidental invocation. HTMX offers an `hx-confirm` attribute that will prompt for confirmation using the old and reliable JavaScript dialog. Many alternative approaches are available and a few examples can be found [here](https://htmx.org/examples/confirm/).

For simplicity, we'll use the built-in `hx-confirm`. The full attribute is `hx-confirm="Are you sure you want to delete this feed?"` and our button becomes:

```html
...
 <button type="submit" 
   class="btn btn-link" 
   aria-label="Delete Feed" 
   title="Delete Feed"
   hx-delete="/Feed/Id/@feed.Id"
   hx-target="closest div.feedEntry"
   hx-swap="outerHTML swap:1s"
   hx-confirm="Are you sure you want to delete this feed?">
  <i class="fa-solid fa-trash gold-txt"></i>
 </button>
...
```

Run the application and test deleting a feed. If you need to re-add the atom feed, the URL is `https://sufficiently-advanced.technology/feed.xml`.

### Wrap-Up

We're beginning to see elements of the dynamic and responsive UX we've been seeking to build in HTML for decades; all without needing to write any JavaScript. This isn't to say HTMX aims to replace JavaScript, there are plenty of instances where client-side functionality is valuable--even necessary--in more complex web applications. We just no longer need verbose and brittle imperative JavaScript or a bloated framework to make our hypermedia application behave in a modern way.

HTMX also returns us to a clean and understandable Locality of Behavior (LOB). Early use of libraries like jQuery often involved separating jQuery code from html. This, in theory, made graceful degradation and progressive enhancement easier but often it made the code more difficult to understand as it was not clear where overriding behavior was being defined and invoked from. HTMX makes this no-longer an either/or proposition.

## Lab 6 - Loading Indicators and UI Blocking (10 mins)

Although we have an entirely AJAX-powered application which now supports varying levels of resource-granularity beyond the default page-level, it would be nice to communicate to the user when something is happening.

HTMX includes `hx-indicator` as an attribute to define an element via a CSS selector to be shown while requests are in-flight. Additionally, child elements to the hypermedia control (like icons inside the form button) with the `htmx-indicator` class applied will be automatically toggled in the absence of an `hx-indicator` property.

> Use Request Indicators!
> Request indicators are an important UX aspect of any distributed application. It is unfortunate that browsers have de-emphasized their native request indicators over time, and it is doubly unfortunate that request indicators are not part of the JavaScript ajax APIs.
>
> Be sure not to neglect this significant aspect of your application. Requests might seem instant when you are working on your application locally, but in the real world they can take quite a bit longer due to network latency. It's often a good idea to take advantage of browser developer tools that allow you to throttle your local browser's response times. This will give you a better idea of what real world users are seeing, and show you where indicators might help users understand exactly what is going on.

### 6.1 - Simple Indicators and Element Blocking

Open `Views/Feed/index.cshtml` and navigate to line 46. We're going to add a simple indicator by adding a spinner, in our case one provided by bootstrap. `<span class="tw-indicator spinner-border gold-txt" role="status" aria-hidden="true"></span>`

```html
...
 <button type="submit" 
   class="btn btn-link" 
   aria-label="Delete Feed" 
   title="Delete Feed"
   hx-delete="/Feed/Id/@feed.Id"
   hx-target="closest div.feedEntry"
   hx-swap="outerHTML swap:1s"
   hx-confirm="Are you sure you want to delete this feed?">
  <i class="fa-solid fa-trash gold-txt"></i>
 </button>
...
```

becomes:

```html
...
 <button type="submit" 
   class="btn btn-link" 
   aria-label="Delete Feed" 
   title="Delete Feed"
   hx-delete="/Feed/Id/@feed.Id"
   hx-target="closest div.feedEntry"
   hx-swap="outerHTML swap:1s"
   hx-confirm="Are you sure you want to delete this feed?">
  <i class="fa-solid fa-trash gold-txt"></i>
  <span class="tw-indicator spinner-border gold-txt" role="status" aria-hidden="true"></span>
 </button>
...
```

By default, HTMX uses opacity to hide or show indicators defined by the class `htmx-indicator`, but I typically prefer to hide the element entirely. We can selectively override these defaults in our CSS by using a custom class. In this case, `tw-indicator`.

```css
...
 .tw-indicator{
  display:none;
 }
 .htmx-request .tw-indicator{
  display:flex;
 }
 .htmx-request.tw-indicator{
  display:flex;
 }
...
```

These rules are already in the workshop's site.css.

Additionally, although our DELETE operation is idempotent, we probably want to prevent multiple submissions. We can accomplish this using the `hx-disabled-elt` attribute to disable individual elements while a request is taking place. In our case, it's the button itself. We'll add `hx-disabled-elt="this"` as another property.

Our button becomes

```html
...
 <button type="submit" 
   class="btn btn-link" 
   aria-label="Delete Feed" 
   title="Delete Feed"
   hx-delete="/Feed/Id/@feed.Id"
   hx-target="closest div.feedEntry"
   hx-swap="outerHTML swap:1s"
   hx-confirm="Are you sure you want to delete this feed?"
   hx-disabled-elt="this">
  <i class="fa-solid fa-trash gold-txt"></i>
  <span class="tw-indicator spinner-border gold-txt" role="status" aria-hidden="true"></span>
 </button>
...
```

Let's get slightly more fancy by hiding the delete icon entirely while the request in in-flight. HTMX will automatically apply the class `htmx-request` to the element while the request is active. We can define the following CSS rules:

```css
...
    .htmx-request .hide-while-loading {
  display: none;
 }
 .htmx-request.hide-while-loading {
  display: none;
 }
...
```

This rule is already in the site CSS, so we only need to add the class to the delete icon. Our button finally becomes:

```html
...
 <button type="submit" 
   class="btn btn-link" 
   aria-label="Delete Feed" 
   title="Delete Feed"
   hx-delete="/Feed/Id/@feed.Id"
   hx-target="closest div.feedEntry"
   hx-swap="outerHTML swap:1s"
   hx-confirm="Are you sure you want to delete this feed?">
  <i class="fa-solid fa-trash gold-txt hide-while-loading"></i>
  <span class="tw-indicator spinner-border gold-txt" role="status" aria-hidden="true"></span>
 </button>
...
```

Run the project again and add/remove feeds. For reference, the example atom feed URL is `https://sufficently-advanced.technology/feed.xml`.

### 6.2 - Apply Spinner and Blocking to "+ Add Feed" Form

By now, you've been introduced to the following HTMX properties:

- `hx-boost`
- `hx-disable`
- `hx-delete`
- `hx-confirm`
- `hx-swap`
- `hx-target`

You understand property inheritance, and you've seen some of the built-in CSS classes HTMX uses.

While turning an individual button into a standalone hypermedia control, forms are often useful. "Add Feed" is a form that must be serialized and posted. In `Views/Feed/Index.cshtml` the form on line 10 is already boosted, but we can add some additional HTMX bells and whistles. In this case, we'll explicitly target specific elements and loader elements.

1. Add the following attributes to the form element:
   - `hx-indicator="#AddFeedIndicator"`
   - `hx-disable-elt="#AddFeedSubmit"`
2. Add the id `AddFeedSubmit` to the submit button
3. Add the following spinner in the same container as the button: `<span id="AddFeedIndicator" class="tw-indicator spinner-border gold-txt" role="status" aria-hidden="true"></span>`

Our form becomes:

```html
...
 <form method="post" class="row g-3">
  <div class="col-auto">
   @Html.LabelFor(m => m.NewUrl, new {@class="col-form-label"})
  </div>
  <div class="col-auto">
   @Html.TextBoxFor(m => m.NewUrl, new {@class="form-control form-control-lg", type="url"})
   @Html.ValidationMessageFor(m => m.NewUrl, "", new {@class="text-danger"})
  </div>
  <div class="col-auto">
   <button type="submit" class="btn btn-light btn-lg" aria-label="Add Feed" title="Add Feed" id="AddFeedSubmit">
    <i class="fa-regular fa-plus"></i> Add Feed
   </button>
   <span id="AddFeedIndicator" class="tw-indicator spinner-border gold-txt" role="status" aria-hidden="true"></span>
  </div>
 </form>
...  
```

Test your dynamic UI and indicators by adding and removing feeds.

## Lab 7 - Paging and Partial Updates (10 mins)

Our feed page is fairly polished. Let's see some more of what HTMX has to offer by bringing the rest of our application up to par. We'll take a look at the post views next.

Requesting a representation of the current state of `/Post` will present us with the first page of posts. Within this resource is an affordance to see the second page. This is currently a Web 1.0 hypermedia control, although the global `hx-boost` attribute will optimize this slightly. Let's tighten the scope of this control by appending posts from page two directly into the current page. To achieve this, we're going to modify the "more" anchor tag, transforming it into a more powerful hypermedia control.

Beginning on line 76 you'll see our current implementation of paging. It's a little basic and ham fisted, but you get the idea.

```html
...
 @if (ViewData["current-page"]?.ToString() != ViewData["next-page"]?.ToString())
 {
  <div class="row">
   <div class="col-12">
    <a href="@ViewData["more-link"]" class="white-txt">more</a>
    <hr class="white-txt top-margin-20 bottom-margin-20">
   </div>
  </div>
 }
...
```

First, you'll see some logic to figure out whether there is an additional page of results. Line 80 contains the anchor we want to work with.

Again, for the purposes of this demo, we're aiming for graceful degradation so we'll keep the unmodified behavior working, but since we have HTMX we can extend the behavior considerably.

To start-with, we want to override the default behavior beyond the inherited `hx-boost` by adding an `hx-get` property to our link. In this case, we'll use the same URL as the `href`.

```html
...
 <a href="@ViewData["more-link"]" class="white-txt">more</a>
...
```

becomes:

```html
...
 <a href="@ViewData["more-link"]" 
    class="white-txt"
    hx-get="@ViewData["more-link"]">more</a>
...
```

By default, the target of any HTMX hypermedia control is the initiating element, so we want to specify an alternate target. In this case, we want to completely replace the paging section with the next page of results (which will include a new "more" link if applicable). Add the property `hx-target="closest div.row"`

```html
...
 <a href="@ViewData["more-link"]" 
  class="white-txt"
  hx-get="@ViewData["more-link"]">more</a>
...
```

becomes:

```html
...
 <a href="@ViewData["more-link"]" 
  class="white-txt"
  hx-get="@ViewData["more-link"]"
  hx-target="closest div.row">more</a>
...
```

Now, we need to define the swap behavior. Because the backend is currently written as a Web 1.0 application, the server will send the full page as a response, we don't actually want to inject the entire page into this div, we just want the posts. We could define custom endpoints, introduce content-negotiation, or make our backend more aware of HTMX; all these options introduce added efficiency by minimizing the response payload. For now, however, we're going to look at a simple alternative.

HTMX gives us a marvelously useful property to extract and swap only part of the response. Although not the most efficient option, it does provide a level of simplicity which can be useful when modernizing a legacy application or supporting full graceful degradation. We're going to use `hx-select` which allows you to select the content you want swapped from a response. In our case, it will be what's inside the list container div with a class of `postList`.

Add `hx-select=".postList` to the properties of the "more" anchor tag.

```html
...
 <a href="@ViewData["more-link"]" 
  class="white-txt"
  hx-get="@ViewData["more-link"]"
  hx-target="closest div.row">more</a>
...
```

becomes:

```html
...
 <a href="@ViewData["more-link"]" 
  class="white-txt"
  hx-get="@ViewData["more-link"]"
  hx-target="closest div.row"
  hx-select=".postList">more</a>
...
```

Finally, we want to completely replace the row containing the "more" link, so we'll use the `outerHTML` swap. Add `hx-swap="outerHTML"` to the anchor tag.

For bonus points (and if time allows) use the techniques from the previous lab to show a loading indicator.

One final note, you'll see the current page URL has not changed. If you want to update the route to page 2, you can add the attribute `hx-push-url="true"` which will keep our location history in sync with our dynamic navigation.

## Lab 8 Triggers, Lazy loading and Events (10 mins)

Let's suppose we wanted to add a feature to our reader. On the "All" navigation item we'd like to see a badge indicating the number of unread posts. We could calculate that during the page rendering on the server, but that would increase the work the server and database have to do before sending a response to the client, reducing user-perceived performance. We could lazy-load that badge after the page loads.

So far, we've created "clickable" hypermedia controls. This is due to the defaults built into HTMX. HTMX AJAX requests are triggered by the �natural� event of an element:

- `input`, `textarea` & `select` are triggered on the `change` event
- `form` is triggered on the submit event
- everything else is triggered by the `click` event

While our badge will likely be a span, we don't want to have to click on it to load our value. HTMX provides a number of [additional triggers.](https://htmx.org/docs/#triggers) Our badge could trigger on a `load` event (or even a `revealed` event, for true lazy loading).

`load` is fine for our purposes. The project already has a partial view and controller method in place. Let's connect HTMX.

### 8.1 Adding a lazy-loaded element

Open `Views/Shared/_Layout.cshtml` our first navigation item begins at line 39:

```html
...
 <a href="@Url.Action("All", "Post")" class="border-end-0 d-inline-block text-truncate navLink" data-bs-parent="#sidebar">
  <i class="fa-regular fa-folder-open gold-txt"></i> <span>All</span>
 </a>
...
```

Next to the nav label, we'll add a span to act as a container with an id of `AllUnreadBadge`. Getting the badge is a `GET` operation, so add an attribute of `hx-get="/Post/Status/Unread/Count"`. Next, since we want to trigger on a `load` event instead of a `click` event, we'll specify our `hx-trigger` with the following attribute `hx-trigger="load"`.

Our navigation item now looks like this:

```html
...
 <a href="@Url.Action("All", "Post")" 
     class="border-end-0 d-inline-block text-truncate navLink" data-bs-parent="#sidebar">
  <i class="fa-regular fa-folder-open gold-txt"></i> <span>All</span>
  <span id="AllUnreadBadge"
    hx-get="/Post/Status/Unread/Count"
    hx-trigger="load">
 </a>
...
```

Now, when the page loads, this count will be asynchronously retrieved.

### 8.2 Custom Events

Currently, most of the navigation in this app uses boosted links, meaning the entire page will be redrawn every time you click on a link (leaving the header intact, save for the page title). Let's use what we've learned to add some more fine-grained navigation.

Open `/Views/Post/List.cshtml` and go to our first boosted link on line 30.

```html
...
 <div class="read-more">
  <a href="/Post/Id/@post.Id" 
  class="rss-btn gold-bg"
  hx-boost="true">Read More</a>
 </div>
...
```

Rather than making this just a simple boosted link, make this an `hx-get` with a target of `#main`, an `hx-select` of `#main`, and we want to swap the outerHTML (replace all of `#main`). Finally, we want our history to be accurate, so be sure to set the `hx-push-url` attribute to true.

Our "read more" link now looks like this:

```html
...
 <div class="read-more">
  <a href="/Post/Id/@post.Id" 
  class="rss-btn gold-bg"
  hx-get="/post/id/@post.Id"
  hx-select="#main"
  hx-target="#main"
  hx-swap="outerHTML"
  hx-push-url="true">Read More</a>
 </div>
...
```

When we navigate to an individual post, we are now only swapping out the content of `#main`, leaving not only the `<head>` intact, we also leave the navigation intact.

The only problem is our read count might be out of date if we just opened an unread post. We only want to trigger that badge to refresh if a post's `IsRead` status has changed. We could maintain a complicated clone of state on the client and introduced the complexity of the flux pattern and trying to keep client/server state in sync. Ultimately, our server is the source of truth and, fortunately, HTMX supports custom events and HTML gives us a global event bus.

One simple way to communicate events to HTMX is using the `HX-Trigger` response header which will emit an event when received. Our badge can also listen for this event.

First, let's produce our custom event.

Open `Controllers/PostController.cs` and head to line 89.

```csharp
...
 if (post is { IsRead: false })
 {
  await _postService.MarkReadAsync(post.Id);
 }
...
```

This line is marking the post as read, only if it is unread. By adding an `HX-Trigger` header to the response, we can specify a custom event.

Add the following after line 89:

```csharp
...
 HttpContext.Response.Headers.Add("HX-Trigger", "postRead");
...
```

Our controller method now looks like this:

```csharp
...
 [HttpGet("/Post/Id/{id}")]
 public async Task<IActionResult> Post(int id)
 {
  var post = await _postService.GetPostAsync(id);
  if(post == null)
   return NotFound();

  if (post is { IsRead: false })
  {
   await _postService.MarkReadAsync(post.Id);
   HttpContext.Response.Headers.Append("HX-Trigger", "postRead");
  }
  
  ViewData["title"] = post.Title;
  ViewData["CurrentUrl"] = $"/Post/Id/{id}";
  
  await Bottleneck();
  return View(post);
 }
...
```

Finally, we can add this event as an additional trigger to our badge.

In `Views/Shared/_Layout.cshtml` change:

```html
...
 <a href="@Url.Action("All", "Post")" 
  class="border-end-0 d-inline-block text-truncate navLink" data-bs-parent="#sidebar">
  <i class="fa-regular fa-folder-open gold-txt"></i> <span>All</span>
  <span id="AllUnreadBadge"
    hx-get="/Post/Status/Unread/Count"
    hx-trigger="load">
 </a>
...
```

to:

```html
...
 <a href="@Url.Action("All", "Post")" 
  class="border-end-0 d-inline-block text-truncate navLink" data-bs-parent="#sidebar">
  <i class="fa-regular fa-folder-open gold-txt"></i> <span>All</span>
  <span id="AllUnreadBadge"
    hx-get="/Post/Status/Unread/Count"
    hx-trigger="load, postRead from:body">
 </a>
...
```

HTMX will dispatch the event on the triggering element, which will bubble up to the body.

View some posts and observe this event in action.

## Lab 9 - The `revealed` Event and Infinite Scroll (10 mins)

Let's look at one more example of HTMX events in action as we implement infinite scroll on our posts list view.

The infinite scroll pattern provides a way to load content dynamically on user scrolling action.

Open `Views/Post/List.cshtml` and head to line 85. Currently our "more" link triggers on the default `click` event. What if we override this default behavior by specifying a trigger?

Add an `hx-trigger` property with a value of `revealed`

Our "more" link section now looks like this:

```html
...
 @if (ViewData["current-page"]?.ToString() != ViewData["next-page"]?.ToString())
 {
  <div class="row">
   <div class="col-12">
    <a href="@ViewData["more-link"]" 
     class="white-txt"
     hx-get="@ViewData["more-link"]"
     hx-target="closest div.row"
     hx-select=".postList"
     hx-indicator="#MorePostsLoading"
     hx-swap="outerHTML"
     hx-trigger="revealed">more</a>
    <span id="MorePostsLoading" class="tw-indicator spinner-border gold-txt" role="status" aria-hidden="true"></span>
    <hr class="white-txt top-margin-20 bottom-margin-20">
   </div>
  </div>
 }
...
```

N.B. (from the docs)

> `revealed` - triggered when an element is scrolled into the viewport (also useful for lazy-loading). If you are using `overflow` in CSS like `overflow-y: scroll` you should use `intersect once` instead of `revealed`.

## Conclusion

In this workshop, you've been introduced to a number of HTMX concepts and gotten hands-on. I wanted to build something a little more interesting that Yet Another ToDo List app. The overall implementation is very quick-and-dirty, I don't use many framework features that would result in a more maintainable codebase. For the purposes of this lab, I wanted to focus as much as possible on the views and the HTMX since the HTMX stack is often referred to as the HOWL (Hypermedia On Whatever you Like) Stack. Use your favorite language and backend framework. Things like minification, bundling, components, etc. are all part of modern server-side HTML frameworks.

Pull requests are welcome.

## More Resources

- [HTMX official docs](https://htmx.org/examples/infinite-scroll/)
- [Hypermedia Systems Book](https://hypermedia.systems/)
- [Awesome HTMX](https://github.com/rajasegar/awesome-htmx)
- [Third Way Web Development (part i)](https://sufficiently-advanced.technology/post/third-way-web-development-part-i)
- [Third Way Web Development (part ii)](https://sufficiently-advanced.technology/post/third-way-web-development-part-ii)

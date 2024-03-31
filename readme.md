# Third Way Web Development Labs/Companion Project

## Introduction

HTML, while powerful, is currently insufficient for building modern and dynamic web applications. As currently specified, only large-grain (full page) interactions are supported. AJAX, popularized with the introduced of [Google Maps](https://maps.google.com) in 2005 demonstrated an approach to leveraging asynchronous requests and DOM manipulation in JavaScript to achieve more fine-grained control over page interactions which introduced the Web 2.0 paradigm. Achieving this is pure JavaScript has historically been challenging. The first challange was inconsistent implementation across browsers.

### The Ajax Library Era

In 2006, a number of javascript libraries emerged including [jQuery](https://jquery.com/), [YUI](https://yui.github.io/yui2/docs/yui_2.9.0_full/) and [ext.js](https://en.wikipedia.org/wiki/Ext_JS). These libraries introduced a browser agnostic API for scripting dynamic interactions and partial page updates. The result was effective but could become unwieldy for large, complex applications. Imperative DOM maniuplation introduced a lot of code to write and manage, and we began to look at approaches to reduce boilerplate, including introducing client-side templating libraries. While some lightweight AJAX libraries remain popular, their APIs have typically seen several breaking changes over time and large amounts of code can be challenging to maintain and test.

### The First Reactive Frameworks

Even with client-side templating, a lot of boilerplate was still necessary. Frameworks began to emerge to abstract and automate DOM manipulations by binding templates to underlying data and observing/reacting to state changes. Although a lot of boilerplate was eliminated, these approaches still required a lot of code to be written and maintained. A lot of these first-generation frameworks moved the then-dominant MVC/MVVM paradigm from the sever to the client which also promised some structure and separation of concerns of client-side code. That said, early reactive frameworks were often not especially opinionated, which resulted in increasingly complex and byzantine client-side appliactions. This code was also irrevicably coupled to framework versions; as frameworks evolved and addressed problems in earlier designs the code had to modified to maintain compatibility with the current version of the frameworks. The overhead and burden of maintenance became increasingly higher. 

### Component-based Frameworks

The next wave of frameworks introduced an alternative approach to structuring and organizing codebases. Applications could be composed of components of varying granularity. This introduced improved modularity, sturcture, and maintainability of code bases. As more and more application logic and behavior could be built and maintained on the client, Single-Page Applications (SPAs) became increasingly common; component-based frameworks simplified this transition. 

The frameworks continued to evolve considerably and, in the process, often introduced breaking changes which further increased the overhead and burden of maintenance. Early versions of these frameworks ignored the expectations of hypermedia which introduced various issues including problems with deep linking, state management, SEO, bookmarking, and progressive enhancement/graceful degratation. As the complexity of these modern frameworks grew, deep specialization became increasingly necessary to work effectively with them. Coupling between client and server is now increasingly common and much application logic has moved to the less trustworthy client environment which further adds complication and complexity.

The latest generation of client-side frameworks have become the defacto best practice for modern web development. Despite the challenges of maintenance, complexity, security, and managing regular breaking changes; this approach has been undenably useful and has enabled much innovation in the web development space.

### HTMX - Hypermedia 2.0

HTMX presents an alternative approach to building modern and dynamic web applications. Hypermedia As The Engine Of Application State (HATEOAS) is a powerful and flexible paradigm that has powered over three-decades of web evolution. The key shortcoming of HTML as a hypermedia is the inflexibility of granularity of interactions. Rather than build a custom API to code against that will inevitably change over time, HTMX seeks to extend HTML as a hypermedia. Developers of HTMX-powered web applications simply write standards-compliant HTML and CSS. Unlike first-gen ajax libraries and modern frameworks; rarely--if ever--will a developer write javascript code to interact with HTMX directly. Instead, HTMX acts as a HTML polyfil to enhance the native capabilities of HTML. Using HTMX simply requires adding additional properties to HTML tags to declaritivly improve behavior. This approach supports both progressive enhancement and graceful degration. The use of declarative tag properties rather than imperative coding also introduces a powerful abstraction allowing HTMX to continue to evolve in meaningful ways without breaking existing implementations in the same way that HTML itself has evolved from 1.0 - 5, never requiring [old pages](https://info.cern.ch/) to be rewritten as new capabilities are introduced.

To borrow a summary from the [official HTMX docs](https://htmx.org/docs/):

HTMX extends and generalizes the core idea of HTML as a hypertext, opening up many more possibilities directly within the language:

- Now any element, not just anchors and forms, can issue an HTTP request
- Now any event, not just clicks or form submissions, can trigger requests
- Now any HTTP verb, not just GET and POST, can be used
- Now any element, not just the entire window, can be the target for update by the request

Note that when you are using htmx, on the server side you typically respond with HTML, not JSON. This keeps you firmly within the [original web programming model](https://www.ics.uci.edu/~fielding/pubs/dissertation/rest_arch_style.htm), using [Hypertext As The Engine Of Application State](https://en.wikipedia.org/wiki/HATEOAS) without even needing to really understand that concept.

It’s worth mentioning that, if you prefer, you can use the data- prefix when using htmx: `<a data-hx-post="/click">Click Me!</a>` (which  will keep your HTML completely valid, only using properties defined in the spec. This is optional, for the purposes of this workshop, we won't be following this convention).

This project provides a playground and an example of how a Web 1.0 application can be modernized to provide the modern, dynamic user experience we have come to expect without adding excessive code that must be organized, maintained, and managed. Applications written using HTMX don't suffer from the complexity, tech debt, and ephemerality of framework-based web apps. This is not to say HTMX applications are superior, simply that a meaningful alternative to framework-centric web apps exists. Web 1.0, framework-centric, and HTMX apps all have their benefits and trade-offs.

#### When to Use HTMX

In short, wherever it makes sense. Some examples might me:

- To modernize a legacy server-side MVC app without a costly and complex rewrite
- An application that must be built and maintained by teams who don't possess significant expertise in any of the mainstream frameworks
- As a less complex and more maintainable approach to building a new web app

## Our Web 1.0 Application - an RSS Reader

Among the interesting ideas of the web that have fallen by the wayside, decentralized publishing may be added to the list. Similar to how frameworks abandoned hypermedia, relagating it to a mere UI definition language (UIML?), and lost many of its benifits in the process; publishing to the web has largely become centralized. One major consequence of this is we ceded control of our content feeds. On social media, for example, I may follow several creators however that relationship is controlled by the publisher. My feed--what I see--is typically algorithmically generated, optimizing for ad revenue and engagement rather than simply providing a single feed of the content from the publishers I want to follow. Like frameworks, it's not all bad; I've discovered much in the process. That said, syndication standards have long allowed us to curate our own feeds which we may follow without manipulation. Really Simple Syndication (RSS) is among the first of many standards to support this.

Our Web 1.0 Application is a very simplistic RSS reader capabible of subscribing to the published feeds of several creators. It is a cross-platform MVC application built using C# and .net 8.0. Like most modern, sever-side web development frameworks, .net core web frameworks support many modern web development tools and concepts including templating, validation, pages, partial views, components, APIs, and security controls. For the sake of simplicity and to keep this workshop as backend framework agnostic as possible, I have tried to keep the implementation simple and use few framework-specific features. However, I want to point out that they do exist and most of what is demonstrated in this lab can be applied to the backend language and framework of your choice.

### Running this application

To run this application locally you'll need:

- A code editor (such as [Visual Studio Code](https://code.visualstudio.com/Download))
- The [.Net 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- A container runtime such as [Docker](https://www.docker.com/products/docker-desktop/) or [Rancher](https://rancherdesktop.io/)

Alternatively, the repo can be developed and debugged entirely online using [GitPod.io](https://gitpod.io/)

## Lab 1 - Getting Set up (10 mins)

Although this can be run entirely locally, if you're not already set up for .net development, I recommend using gitpod.

1. Head to [GitPod.io](https://gitpod.io/) and log in using your github, gitlab, or bitbucket credentials.
1. Click "New Workspace" 
1. Click "Select a Repository" and enter the repo address (`https://github.com/carducci/ThirdWayWorkshop`) and create the workspace.

At this point, an in-browser instance of visual studio code will be launched and configured for this development environment.

First we'll restore any packages and dependencies. In the terminal, type the following (remember the linux filesystem is case sensitive):

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

A single view defines the UI for all Post lists, `/Views/Post/List.cshtml`. Additionaly, `Post.cshtml` defines the UI for viewing a single post.

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

HTMX is a single lightweight (~14kb gzipped) and depedency-free library. In most cases it will be installed by adding a `<script>` tag to your html template, however in can be installed using JavaScript package managers (e.g. `npm install htmx.org`). Since a build step is not necessary for this project, we can simply reference the library from a CDN or download it to `/wwwroot/lib`.

1. Open `/Views/Shared/_Layout.cshtml`
2. Add the following `<script>` tag to the `<head>`

```html
<script src="https://unpkg.com/htmx.org@1.9.11" integrity="sha384-0gxUXCCR8yv9FM2b+U3FDbsKthCI66oH5IA9fHppQq9DDMHuMauqq1ZHBpJxQ0J0" crossorigin="anonymous"></script>
```

N.B. When referencing external files such as this, always define `crossorigin="anonymous"` 

That's it! The entire application is now using Fielding's code-on-demand constraint to make HTML a significantly more powerful hypermedia.

Verify HTMX has loaded correctly by running the application and typing `htmx.version` in the console.

## Lab 3 - Introducing hx-boost (10 mins)

One advantage Single-Page Applications have over Web 1.0 applications is their ability to perform page navigation without necessarily throwing away the entire DOM, CSS rules, and javascript files. A full-page change often requires retrieving these resources again (admittedly, often from cache) and parsing/executing them during every navigation event.

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

In many cases, this is behavior we might wish to deploy across our application. While this could be accomplished by adding the `hx-boost="true"` property to every hyperlink and form, HTMX also supports attribute inheritance, what the authors refert to as "cascading HTMX attributes" inspired by "cascading style sheets."

Let's apply boosting globally by adding this property to the `<body>` tag in `Views/Shared/_layout.cshtml` on line 12.

```html
<body>
```

becomes

```html
<body hx-boost="true">
```

Save and run the application to verify that all local interactions are boosted. You'll likely note as you navigate around that, despite navigation within the app being entirely ajax, HTMX is maintaining the correct location within the browser. Back and Forward buttons will work as expected, as will bookmarks. HTMX provides sensible defauts for when the location bar should be updated, but you can override these defaults if/when it makes sense. We'll explore this in upcoming labs.

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

## Lab 5 - Beyond GET and POST and Enhanced UX

The Uniform Interface constraint of REST focuses on a handful of primary components:

- The Resource Abstraction
- Stable URIs as Resource Identifiers
- Semantically constrained interactions driven by passing representations of resources back and forth

In general, this application follows a consistent pattern for resource identifiers as follows:

`/{collection/class}/{collection sub-partition}/{sub-partition identifier}`

We have `/Post` which will retrieve a collection of posts.
We have `/Post/Id/1` which will retrieve an individual post instance.

Likewise we have `/Feed` which will retrieve a collection of feeds.

However, the current HTML standard only supports `GET` and `POST` operations within the built-in hypermedia controls, which means we have to deviate from the uniform interface somewhat and introduce an awkward endpoint `/Feed/Id/1/Delete` which is accessed with a POST operation. We are nolonger so constrained as HTMX allows us to perform a semantically consistent `DELETE` operation.

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

becomes 

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

In this case, the button itself becomes an independant, standalone hypermedia control. When HTMX is running the form itself becomes irrelevant. This is a key capability of HTMX, to enable any element to become a hypermedia control and fully participate in a hypermedia driven application. 

This implementation will gracefully degrade, however if this is not neccessary, the wrapping form and the obsolete method may simply be removed.

### 5.3 Adding Client-Side Behavior

The obsolete Delete method will return a new representation of the `/Feed` collection, but a `DELETE` method does not return a reperesentation of any resource, just a `204` response indicating the request was successful but there is no content to return.

We won't see a change in state of the `/Feed` resource until we make a new request by refreshing the page or navigating to the "Feeds" view from the left-nav.

While our hypermedia interactions are still fairly large-grain, we can get much more fine-grained using the `hx-target` attribute. This defines the scope of the resource we are interacting with and permits very fine-grained interaction. In this case, the `/Feed` resource is a composite of all the invidual Feed resources. The target resource is the parent div of the entry in the list, which we can identify as the closest div with a class of "row".

The `hx-target` attribute will swap the content of the identified element with whatever the response is. In our case no content is returned so the entire row will be removed. 

N.B. As of the current versions of HTMX, a [204 response is interpreted as requiring no action](https://github.com/bigskysoftware/htmx/issues/1130) (since there is no response body). Only successful responses which are defined as having a body will trigger a swap operation. Since we are aiming to provide an idiomatic uniform interface for our application, I have modifed the configuration of HTMX to instruct HTMX to swap on 204 responses. You can see this modification in `wwwroot/js/site.js` and read more about this [here](https://htmx.org/docs/#modifying_swapping_behavior_with_events).

By default, `hx-target` will swap the inner html of the element, but here we can remove the entire row div so we want to replace the outer html. We can further extend the swap operation with a css animation to fade the row out.

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

HTMX will automatically apply an `hx-swapping` class during the swap operation (which we've added a delay to to support a fadeout transition). In the application css, this rule is defined as follows:

```css
...
	div.feedEntry.htmx-swapping{
		opacity: 0;
		transition: opacity 1s ease-out;
	}
...
```

Since a `DELETE` operation has destructive side-effects, we probably want to guard against accidental invokation. HTMX offers an `hx-confirm` attribute that will prompt for confirmation using the old and reliable javascript dialog. Many alternative approaches are available and a few examples can be found [here](https://htmx.org/examples/confirm/).

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

Run the application and test deleting a feed. If you need to re-add the atom feed, the url is `https://sufficiently-advanced.technology/feed.xml`.

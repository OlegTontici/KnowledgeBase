using System.Collections.Generic;
using System.Linq;

namespace KnowledgeBase.Resources
{
    public static class SmartThoughts
    {
        private static IList<SmartThought> Data = new List<SmartThought>
        {
            new SmartThought{ Tags = new List<string> { "DDD" }, Title = "DDD" , Preview = "Easing (or timing function) is what makes animations and motions look natural and clean. Nohthing really moves linearly in our physical world or else will look unnatural. Motions that are linear usually trigger suspicion in our brain. When we open a drawer or throw a ball, there is always a change in the speed over time and in general we specify the progress or the rate of change of a parameter over time using easing functions.Following is the list of some default easing functions and also you can utilize the bezier curve to create your own custom versions.", FormattedContent = @"# Markdown.Xaml #

Markdown XAML is a port of the popular *MarkdownSharp* Markdown processor, but 
with one very significant difference: Instead of rendering to a string 
containing HTML, it renders to a FlowDocument suitable for embedding into a 
WPF window or usercontrol.

## Features ##

MarkDown.Xaml has a number of convenient features

* The engine itself is a single file, for easy inclusion in your own projects
* Code for the engine is structured in the same manner as the original MarkdownSharp  
If there are any bug fixes to the regular expressions in MarkdownSharp, merging those fixes in the Markdown.Xaml should be straightforward
* Includes a `TextToFlowDocumentConverter` to make it easy to bind Markdown text


## Markdown capabilities and customizables styles ##

* Links [Go to Google!](https://www.google.com)
* Remote images

![image1](http://placehold.it/350x150)

![imageleft](http://placehold.it/100x150/0000FF)![imageright](http://placehold.it/100x150/00FFFF)

* Local images

![localimage](sampleimage.jpg)

* Table

table begin string
|a|b|c|d|
|:-:|:-|-:|
|a1234567890|b1234567890|c1234567890|d1234567890|
|a|b|c|d|
table end string

This table is pre-formatted in text 

|    Alpha    | Beta        |       Gamma | Delta       |
|:-----------:|:------------|------------:|
| a1234567890 | b1234567890 | c1234567890 | d1234567890 |
|a            | b           |    c        |           d |


* Separator
***


## What is this Demo? ##

This demo application shows MarkDown.Xaml in use - as you make changes to the 
left pane, the rendered MarkDown will appear in the right pane.

### Source ###

Review the source for this demo to see how MarkDown.Xaml works in practice, how to use the TextToFlowDocumentConverter,
and how to style the output to appear the way you desire.


" },
                new SmartThought{ Tags = new List<string> {"DDD", "CQRS" }, Title = "CQRS", Preview = "Easing (or timing function) is what makes animations and motions look natural and clean. Nohthing really moves linearly in our physical world or else will look unnatural. Motions that are linear usually trigger suspicion in our brain. When we open a drawer or throw a ball, there is always a change in the speed over time and in general we specify the progress or the rate of change of a parameter over time using easing functions.Following is the list of some default easing functions and also you can utilize the bezier curve to create your own custom versions.", FormattedContent = @"# Markdown.Xaml #

Markdown XAML is a port of the popular *MarkdownSharp* Markdown processor, but 
with one very significant difference: Instead of rendering to a string 
containing HTML, it renders to a FlowDocument suitable for embedding into a 
WPF window or usercontrol.

## Features ##

MarkDown.Xaml has a number of convenient features

* The engine itself is a single file, for easy inclusion in your own projects
* Code for the engine is structured in the same manner as the original MarkdownSharp  
If there are any bug fixes to the regular expressions in MarkdownSharp, merging those fixes in the Markdown.Xaml should be straightforward
* Includes a `TextToFlowDocumentConverter` to make it easy to bind Markdown text


## Markdown capabilities and customizables styles ##

* Links [Go to Google!](https://www.google.com)
* Remote images

![image1](http://placehold.it/350x150)

![imageleft](http://placehold.it/100x150/0000FF)![imageright](http://placehold.it/100x150/00FFFF)

* Local images

![localimage](sampleimage.jpg)

* Table

table begin string
|a|b|c|d|
|:-:|:-|-:|
|a1234567890|b1234567890|c1234567890|d1234567890|
|a|b|c|d|
table end string

This table is pre-formatted in text 

|    Alpha    | Beta        |       Gamma | Delta       |
|:-----------:|:------------|------------:|
| a1234567890 | b1234567890 | c1234567890 | d1234567890 |
|a            | b           |    c        |           d |


* Separator
***


## What is this Demo? ##

This demo application shows MarkDown.Xaml in use - as you make changes to the 
left pane, the rendered MarkDown will appear in the right pane.

### Source ###

Review the source for this demo to see how MarkDown.Xaml works in practice, how to use the TextToFlowDocumentConverter,
and how to style the output to appear the way you desire.


" },
                new SmartThought{ Tags = new List<string> { "Other" }, Title = "WMS", Preview = "Easing (or timing function) is what makes animations and motions look natural and clean. Nohthing really moves linearly in our physical world or else will look unnatural. Motions that are linear usually trigger suspicion in our brain. When we open a drawer or throw a ball, there is always a change in the speed over time and in general we specify the progress or the rate of change of a parameter over time using easing functions.Following is the list of some default easing functions and also you can utilize the bezier curve to create your own custom versions." },

        };

        public static IList<SmartThought> GetAll()
        {
            return Data;
        }

        public static IList<SmartThought> GetByTags(IList<string> tags)
        {
            return Data.Where(x => x.Tags.Any(y => tags.Contains(y))).ToList();
        }

        public static void Upsert(SmartThought smartThought)
        {
            if(!Data.Any(x => x.Id == smartThought.Id))
            {
                Data.Add(smartThought);
            }
        }
    }
}

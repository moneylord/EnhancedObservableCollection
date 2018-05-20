# EnhancedObservableCollection

Observable Collection is updated whenever CollectionItem is Changed.
Since the screen is updated whenever there are many items added to 'Collection', there may be performance problems.
When a large amount of data is added to 'Collection', this method is to update after being added without updating the screen.

ObservableCollection Class의 'OnCollectionChanged' method를 override하여 
boolean Property flag에 따라 Event 발생을 제한하고 아이템이 모두 추가되었을 때 refresh Method를 호출하는 방식으로 사용.
다만, 적은 수의 아이템을 추가하는 경우에는 속도 체감을 하기 힘들다.

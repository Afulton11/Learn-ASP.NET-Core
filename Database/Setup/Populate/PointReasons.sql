USE SimpleDatabase;
GO

INSERT Blog.PointReasons (Reason)
VALUES
    (N'Published an article'),
    (N'Favorited a comment'),
    (N'Commented on an article'),
    (N'Replied to a new comment thread'),
    (N'Been on the website for atleast 10 minutes');

SELECT *
FROM Blog.PointReasons;
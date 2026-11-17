type: fix

`MaximumItemCount` is now checked before the next record, source or upstream item is consumed, so the item after the limit can no longer fail the run or be pulled from upstream.

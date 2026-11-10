type: internal

`RecordTypeInfo<TRecord>` now defines its trim/AOT warning text only on the target frameworks whose `[RequiresUnreferencedCode]` / `[RequiresDynamicCode]` attributes use it, which clears the two InspectCode unused-member findings on the older frameworks; no behaviour or public surface changes.

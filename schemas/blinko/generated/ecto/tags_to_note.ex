defmodule Blinko.TagsToNote do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          note_id: term(),
          tag_id: term(),
          id: integer(),
          note: Blinko.Notes.t() | nil,
          tag: Blinko.Tag.t() | nil
        }

  schema "tagsToNote" do
    field(:id, :integer)
    field(:note_id, :id, primary_key: true, default: 0, source: :noteId)
    field(:tag_id, :id, primary_key: true, default: 0, source: :tagId)
    belongs_to(:note, Blinko.Notes, foreign_key: :note_id, define_field: false)
    belongs_to(:tag, Blinko.Tag, foreign_key: :tag_id, define_field: false)
  end
end
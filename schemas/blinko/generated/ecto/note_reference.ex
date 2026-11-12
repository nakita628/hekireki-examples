defmodule Blinko.NoteReference do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          inserted_at: DateTime.t(),
          from_note: Blinko.Notes.t() | nil,
          to_note: Blinko.Notes.t() | nil
        }

  schema "noteReference" do
    field(:from_note_id, :id, source: :fromNoteId)
    field(:to_note_id, :id, source: :toNoteId)
    belongs_to(:from_note, Blinko.Notes, foreign_key: :from_note_id, define_field: false)
    belongs_to(:to_note, Blinko.Notes, foreign_key: :to_note_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end
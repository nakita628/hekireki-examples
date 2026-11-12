defmodule Blinko.NoteHistory do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          content: String.t(),
          metadata: map() | nil,
          version: integer(),
          account_id: integer() | nil,
          inserted_at: DateTime.t(),
          note: Blinko.Notes.t() | nil
        }

  schema "noteHistory" do
    field(:content, :string)
    field(:metadata, :map)
    field(:version, :integer)
    field(:account_id, :integer, source: :accountId)
    field(:note_id, :id, source: :noteId)
    belongs_to(:note, Blinko.Notes, foreign_key: :note_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end
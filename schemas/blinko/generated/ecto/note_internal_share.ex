defmodule Blinko.NoteInternalShare do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          can_edit: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          note: Blinko.Notes.t() | nil,
          account: Blinko.Accounts.t() | nil
        }

  schema "noteInternalShare" do
    field(:can_edit, :boolean, default: true, source: :canEdit)
    field(:note_id, :id, source: :noteId)
    field(:account_id, :id, source: :accountId)
    belongs_to(:note, Blinko.Notes, foreign_key: :note_id, define_field: false)
    belongs_to(:account, Blinko.Accounts, foreign_key: :account_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
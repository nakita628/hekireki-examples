defmodule Blinko.Comments do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          content: String.t(),
          guest_name: String.t() | nil,
          guest_ip: String.t() | nil,
          guest_ua: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          note: Blinko.Notes.t() | nil,
          account: Blinko.Accounts.t() | nil,
          parent: Blinko.Comments.t() | nil,
          replies: [Blinko.Comments.t()]
        }

  schema "comments" do
    field(:content, :string)
    field(:guest_name, :string, source: :guestName)
    field(:guest_ip, :string, source: :guestIP)
    field(:guest_ua, :string, source: :guestUA)
    field(:note_id, :id, source: :noteId)
    field(:account_id, :id, source: :accountId)
    field(:parent_id, :id, source: :parentId)
    belongs_to(:note, Blinko.Notes, foreign_key: :note_id, define_field: false)
    belongs_to(:account, Blinko.Accounts, foreign_key: :account_id, define_field: false)
    belongs_to(:parent, Blinko.Comments, foreign_key: :parent_id, define_field: false)
    has_many(:replies, Blinko.Comments, foreign_key: :parent_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
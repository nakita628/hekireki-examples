defmodule Blinko.Tag do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          icon: String.t(),
          parent: integer(),
          sort_order: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          account: Blinko.Accounts.t() | nil,
          tags_to_note: [Blinko.TagsToNote.t()]
        }

  schema "tag" do
    field(:name, :string, default: "")
    field(:icon, :string, default: "")
    field(:parent, :integer, default: 0)
    field(:sort_order, :integer, default: 0, source: :sortOrder)
    field(:account_id, :id, source: :accountId)
    belongs_to(:account, Blinko.Accounts, foreign_key: :account_id, define_field: false)
    has_many(:tags_to_note, Blinko.TagsToNote, foreign_key: :tag_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
defmodule Blinko.Attachments do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          is_share: boolean(),
          share_password: String.t(),
          name: String.t(),
          path: String.t(),
          size: Decimal.t(),
          type: String.t(),
          sort_order: integer(),
          perfix_path: String.t() | nil,
          depth: integer() | nil,
          metadata: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          note: Blinko.Notes.t() | nil,
          account: Blinko.Accounts.t() | nil
        }

  schema "attachments" do
    field(:is_share, :boolean, default: false, source: :isShare)
    field(:share_password, :string, default: "", source: :sharePassword)
    field(:name, :string, default: "")
    field(:path, :string, default: "")
    field(:size, :decimal, default: Decimal.new("0"))
    field(:type, :string, default: "")
    field(:sort_order, :integer, default: 0, source: :sortOrder)
    field(:perfix_path, :string, default: "", source: :perfixPath)
    field(:depth, :integer)
    field(:metadata, :map)
    field(:note_id, :id, source: :noteId)
    field(:account_id, :id, source: :accountId)
    belongs_to(:note, Blinko.Notes, foreign_key: :note_id, define_field: false)
    belongs_to(:account, Blinko.Accounts, foreign_key: :account_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
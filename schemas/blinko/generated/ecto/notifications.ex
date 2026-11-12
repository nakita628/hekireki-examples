defmodule Blinko.Notifications do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          type: String.t(),
          title: String.t(),
          content: String.t(),
          metadata: map() | nil,
          is_read: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          account: Blinko.Accounts.t() | nil
        }

  schema "notifications" do
    field(:type, :string)
    field(:title, :string)
    field(:content, :string)
    field(:metadata, :map)
    field(:is_read, :boolean, default: false, source: :isRead)
    field(:account_id, :id, source: :accountId)
    belongs_to(:account, Blinko.Accounts, foreign_key: :account_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
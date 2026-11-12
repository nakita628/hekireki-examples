defmodule Blinko.Conversation do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          title: String.t(),
          is_share: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          account: Blinko.Accounts.t() | nil,
          messages: [Blinko.Message.t()]
        }

  schema "conversation" do
    field(:title, :string, default: "")
    field(:is_share, :boolean, default: false, source: :isShare)
    field(:account_id, :id, source: :accountId)
    belongs_to(:account, Blinko.Accounts, foreign_key: :account_id, define_field: false)
    has_many(:messages, Blinko.Message, foreign_key: :conversation_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
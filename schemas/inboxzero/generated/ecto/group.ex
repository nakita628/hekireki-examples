defmodule InboxZero.Group do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          prompt: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil,
          rule: InboxZero.Rule.t() | nil,
          items: [InboxZero.GroupItem.t()]
        }

  schema "Group" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:prompt, :string)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    has_one(:rule, InboxZero.Rule, foreign_key: :group_id, references: :id)
    has_many(:items, InboxZero.GroupItem, foreign_key: :group_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
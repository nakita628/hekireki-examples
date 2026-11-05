defmodule InboxZero.Category do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil,
          email_senders: [InboxZero.Newsletter.t()],
          rules: [InboxZero.Rule.t()]
        }

  schema "Category" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    has_many(:email_senders, InboxZero.Newsletter, foreign_key: :category_id, references: :id)
    many_to_many(:rules, InboxZero.Rule, join_through: "_CategoryToRule", join_keys: [A: :id, B: :id])
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
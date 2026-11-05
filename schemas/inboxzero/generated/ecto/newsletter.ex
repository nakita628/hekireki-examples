defmodule InboxZero.Newsletter do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t(),
          name: String.t() | nil,
          status: atom() | nil,
          pattern_analyzed: boolean(),
          last_analyzed_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil,
          category: InboxZero.Category.t() | nil
        }

  schema "Newsletter" do
    field(:id, :string, primary_key: true)
    field(:email, :string)
    field(:name, :string)
    field(:status, Ecto.Enum, values: [:APPROVED, :UNSUBSCRIBED, :AUTO_ARCHIVED])
    field(:pattern_analyzed, :boolean, default: false, source: :patternAnalyzed)
    field(:last_analyzed_at, InboxZero.PrismaDateTime, source: :lastAnalyzedAt)
    field(:email_account_id, :string, source: :emailAccountId)
    field(:category_id, :string, source: :categoryId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    belongs_to(:category, InboxZero.Category, foreign_key: :category_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
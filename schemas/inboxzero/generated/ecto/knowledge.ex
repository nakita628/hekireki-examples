defmodule InboxZero.Knowledge do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          title: String.t(),
          content: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "Knowledge" do
    field(:id, :string, primary_key: true)
    field(:title, :string)
    field(:content, :string)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
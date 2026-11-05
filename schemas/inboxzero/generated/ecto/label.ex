defmodule InboxZero.Label do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          gmail_label_id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          enabled: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "Label" do
    field(:id, :string, primary_key: true)
    field(:gmail_label_id, :string, source: :gmailLabelId)
    field(:name, :string)
    field(:description, :string)
    field(:enabled, :boolean, default: true)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
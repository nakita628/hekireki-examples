defmodule InboxZero.MailSplit do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          order: integer(),
          match_all: boolean(),
          bundle_in_inbox: boolean(),
          hide_from_all: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil,
          filters: [InboxZero.MailSplitFilter.t()]
        }

  schema "MailSplit" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:order, :integer)
    field(:match_all, :boolean, default: true, source: :matchAll)
    field(:bundle_in_inbox, :boolean, default: false, source: :bundleInInbox)
    field(:hide_from_all, :boolean, default: false, source: :hideFromAll)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    has_many(:filters, InboxZero.MailSplitFilter, foreign_key: :mail_split_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
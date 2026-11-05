defmodule InboxZero.PendingPremiumGrant do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t(),
          tier: atom(),
          count: integer(),
          email_accounts_access: integer() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "PendingPremiumGrant" do
    field(:id, :string, primary_key: true)
    field(:email, :string)
    field(:tier, Ecto.Enum, values: [BASIC_MONTHLY: "BASIC_MONTHLY", BASIC_ANNUALLY: "BASIC_ANNUALLY", PRO_MONTHLY: "PRO_MONTHLY", PRO_ANNUALLY: "PRO_ANNUALLY", STARTER_MONTHLY: "BUSINESS_MONTHLY", STARTER_ANNUALLY: "BUSINESS_ANNUALLY", PLUS_MONTHLY: "PLUS_MONTHLY", PLUS_ANNUALLY: "PLUS_ANNUALLY", PROFESSIONAL_MONTHLY: "BUSINESS_PLUS_MONTHLY", PROFESSIONAL_ANNUALLY: "BUSINESS_PLUS_ANNUALLY", COPILOT_MONTHLY: "COPILOT_MONTHLY", LIFETIME: "LIFETIME"])
    field(:count, :integer, default: 1)
    field(:email_accounts_access, :integer, source: :emailAccountsAccess)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
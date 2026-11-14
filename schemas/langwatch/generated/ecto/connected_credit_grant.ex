defmodule LangWatch.ConnectedCreditGrant do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          account_id: String.t(),
          stripe_credit_grant_id: String.t(),
          amount_usd_cents: integer(),
          kind: atom(),
          term_ends_at: DateTime.t(),
          expires_at: DateTime.t(),
          inserted_at: DateTime.t()
        }

  schema "ConnectedCreditGrant" do
    field(:id, :string, primary_key: true)
    field(:account_id, :string, source: :accountId)
    field(:stripe_credit_grant_id, :string, source: :stripeCreditGrantId)
    field(:amount_usd_cents, :integer, source: :amountUsdCents)
    field(:kind, Ecto.Enum, values: [:commit, :added, :renewal])
    field(:term_ends_at, LangWatch.PrismaDateTime, source: :termEndsAt)
    field(:expires_at, LangWatch.PrismaDateTime, source: :expiresAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end
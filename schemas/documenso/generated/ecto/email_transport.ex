defmodule Documenso.EmailTransport do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          type: atom(),
          from_name: String.t(),
          from_address: String.t(),
          config: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          subscription_claims: [Documenso.SubscriptionClaim.t()],
          organisation_claims: [Documenso.OrganisationClaim.t()]
        }

  schema "EmailTransport" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:type, Ecto.Enum, values: [:SMTP_AUTH, :SMTP_API, :RESEND, :MAILCHANNELS])
    field(:from_name, :string, source: :fromName)
    field(:from_address, :string, source: :fromAddress)
    field(:config, :string)
    has_many(:subscription_claims, Documenso.SubscriptionClaim, foreign_key: :email_transport_id)
    has_many(:organisation_claims, Documenso.OrganisationClaim, foreign_key: :email_transport_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
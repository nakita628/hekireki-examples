defmodule Calcom.OAuthClient do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          client_id: String.t(),
          redirect_uri: String.t(),
          client_secret: String.t() | nil,
          client_type: atom(),
          name: String.t(),
          purpose: String.t() | nil,
          logo: String.t() | nil,
          website_url: String.t() | nil,
          rejection_reason: String.t() | nil,
          is_trusted: boolean(),
          status: atom(),
          inserted_at: DateTime.t(),
          user: Calcom.User.t() | nil,
          access_codes: [Calcom.AccessCode.t()]
        }

  schema "OAuthClient" do
    field(:client_id, :string, primary_key: true, source: :clientId)
    field(:redirect_uri, :string, source: :redirectUri)
    field(:client_secret, :string, source: :clientSecret)
    field(:client_type, Ecto.Enum, values: [CONFIDENTIAL: "confidential", PUBLIC: "public"], default: :CONFIDENTIAL, source: :clientType)
    field(:name, :string)
    field(:purpose, :string)
    field(:logo, :string)
    field(:website_url, :string, source: :websiteUrl)
    field(:rejection_reason, :string, source: :rejectionReason)
    field(:is_trusted, :boolean, default: false, source: :isTrusted)
    field(:status, Ecto.Enum, values: [PENDING: "pending", APPROVED: "approved", REJECTED: "rejected"], default: :APPROVED)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    has_many(:access_codes, Calcom.AccessCode, foreign_key: :client_id, references: :client_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end
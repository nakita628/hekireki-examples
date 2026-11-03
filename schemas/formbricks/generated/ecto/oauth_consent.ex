defmodule Formbricks.OauthConsent do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          reference_id: String.t() | nil,
          scopes: [String.t()],
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          resources: [String.t()],
          requested_user_info_claims: [String.t()],
          client: Formbricks.OauthClient.t() | nil,
          user: Formbricks.User.t() | nil
        }

  schema "oauthConsent" do
    field(:id, :string, primary_key: true)
    field(:reference_id, :string, source: :referenceId)
    field(:scopes, {:array, :string})
    field(:created_at, Formbricks.PrismaDateTime, source: :createdAt)
    field(:updated_at, Formbricks.PrismaDateTime, source: :updatedAt)
    field(:resources, {:array, :string})
    field(:requested_user_info_claims, {:array, :string}, source: :requestedUserInfoClaims)
    field(:client_id, :string, source: :clientId)
    field(:user_id, :string, source: :userId)
    belongs_to(:client, Formbricks.OauthClient, foreign_key: :client_id, define_field: false, type: :string, references: :client_id)
    belongs_to(:user, Formbricks.User, foreign_key: :user_id, define_field: false, type: :string)
  end
end
defmodule LangWatch.SsoProvider do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          issuer: String.t(),
          oidc_config: String.t() | nil,
          saml_config: String.t() | nil,
          user_id: String.t() | nil,
          provider_id: String.t(),
          organization_id: String.t() | nil,
          domain: String.t()
        }

  schema "SsoProvider" do
    field(:id, :string, primary_key: true)
    field(:issuer, :string)
    field(:oidc_config, :string, source: :oidcConfig)
    field(:saml_config, :string, source: :samlConfig)
    field(:user_id, :string, source: :userId)
    field(:provider_id, :string, source: :providerId)
    field(:organization_id, :string, source: :organizationId)
    field(:domain, :string)
  end
end
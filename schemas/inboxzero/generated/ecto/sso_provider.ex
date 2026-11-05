defmodule InboxZero.SsoProvider do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          issuer: String.t(),
          oidc_config: String.t() | nil,
          saml_config: String.t() | nil,
          provider_id: String.t(),
          domain: String.t(),
          user: InboxZero.User.t() | nil,
          email_account: InboxZero.EmailAccount.t() | nil,
          organization: InboxZero.Organization.t() | nil
        }

  schema "ssoProvider" do
    field(:id, :string, primary_key: true)
    field(:issuer, :string)
    field(:oidc_config, :string, source: :oidcConfig)
    field(:saml_config, :string, source: :samlConfig)
    field(:provider_id, :string, source: :providerId)
    field(:domain, :string)
    field(:user_id, :string, source: :userId)
    field(:email_account_id, :string, source: :emailAccountId)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:user, InboxZero.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    belongs_to(:organization, InboxZero.Organization, foreign_key: :organization_id, define_field: false, type: :string)
  end
end
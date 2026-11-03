defmodule Calcom.PlatformOAuthClient do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          secret: String.t(),
          permissions: integer(),
          logo: String.t() | nil,
          redirect_uris: [String.t()],
          booking_redirect_uri: String.t() | nil,
          booking_cancel_redirect_uri: String.t() | nil,
          booking_reschedule_redirect_uri: String.t() | nil,
          are_emails_enabled: boolean(),
          are_default_event_types_enabled: boolean(),
          are_calendar_events_enabled: boolean(),
          inserted_at: DateTime.t(),
          organization: Calcom.Team.t() | nil,
          teams: [Calcom.Team.t()],
          access_tokens: [Calcom.AccessToken.t()],
          refresh_token: [Calcom.RefreshToken.t()],
          authorization_tokens: [Calcom.PlatformAuthorizationToken.t()],
          webhook: [Calcom.Webhook.t()],
          users: [Calcom.User.t()]
        }

  schema "PlatformOAuthClient" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:secret, :string)
    field(:permissions, :integer)
    field(:logo, :string)
    field(:redirect_uris, {:array, :string}, source: :redirectUris)
    field(:booking_redirect_uri, :string, source: :bookingRedirectUri)
    field(:booking_cancel_redirect_uri, :string, source: :bookingCancelRedirectUri)
    field(:booking_reschedule_redirect_uri, :string, source: :bookingRescheduleRedirectUri)
    field(:are_emails_enabled, :boolean, default: false, source: :areEmailsEnabled)
    field(:are_default_event_types_enabled, :boolean, default: true, source: :areDefaultEventTypesEnabled)
    field(:are_calendar_events_enabled, :boolean, default: true, source: :areCalendarEventsEnabled)
    field(:organization_id, :id, source: :organizationId)
    belongs_to(:organization, Calcom.Team, foreign_key: :organization_id, define_field: false)
    has_many(:teams, Calcom.Team, foreign_key: :created_by_oauth_client_id)
    has_many(:access_tokens, Calcom.AccessToken, foreign_key: :platform_oauth_client_id)
    has_many(:refresh_token, Calcom.RefreshToken, foreign_key: :platform_oauth_client_id)
    has_many(:authorization_tokens, Calcom.PlatformAuthorizationToken, foreign_key: :platform_oauth_client_id)
    has_many(:webhook, Calcom.Webhook, foreign_key: :platform_oauth_client_id)
    many_to_many(:users, Calcom.User, join_through: "_PlatformOAuthClientToUser", join_keys: [A: :id, B: :id])
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end
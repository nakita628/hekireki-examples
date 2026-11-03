defmodule Calcom.SelectedCalendar do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          integration: String.t(),
          external_id: String.t(),
          google_channel_id: String.t() | nil,
          google_channel_kind: String.t() | nil,
          google_channel_resource_id: String.t() | nil,
          google_channel_resource_uri: String.t() | nil,
          google_channel_expiration: String.t() | nil,
          channel_id: String.t() | nil,
          channel_kind: String.t() | nil,
          channel_resource_id: String.t() | nil,
          channel_resource_uri: String.t() | nil,
          channel_expiration: DateTime.t() | nil,
          sync_subscribed_at: DateTime.t() | nil,
          sync_subscribed_error_at: DateTime.t() | nil,
          sync_subscribed_error_count: integer(),
          sync_token: String.t() | nil,
          synced_at: DateTime.t() | nil,
          sync_error_at: DateTime.t() | nil,
          sync_error_count: integer() | nil,
          error: String.t() | nil,
          last_error_at: DateTime.t() | nil,
          watch_attempts: integer(),
          unwatch_attempts: integer(),
          max_attempts: integer(),
          inserted_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          user: Calcom.User.t() | nil,
          credential: Calcom.Credential.t() | nil,
          delegation_credential: Calcom.DelegationCredential.t() | nil,
          event_type: Calcom.EventType.t() | nil,
          calendar_cache_events: [Calcom.CalendarCacheEvent.t()]
        }

  schema "SelectedCalendar" do
    field(:integration, :string)
    field(:external_id, :string, source: :externalId)
    field(:google_channel_id, :string, source: :googleChannelId)
    field(:google_channel_kind, :string, source: :googleChannelKind)
    field(:google_channel_resource_id, :string, source: :googleChannelResourceId)
    field(:google_channel_resource_uri, :string, source: :googleChannelResourceUri)
    field(:google_channel_expiration, :string, source: :googleChannelExpiration)
    field(:channel_id, :string, source: :channelId)
    field(:channel_kind, :string, source: :channelKind)
    field(:channel_resource_id, :string, source: :channelResourceId)
    field(:channel_resource_uri, :string, source: :channelResourceUri)
    field(:channel_expiration, Calcom.PrismaDateTime, source: :channelExpiration)
    field(:sync_subscribed_at, Calcom.PrismaDateTime, source: :syncSubscribedAt)
    field(:sync_subscribed_error_at, Calcom.PrismaDateTime, source: :syncSubscribedErrorAt)
    field(:sync_subscribed_error_count, :integer, default: 0, source: :syncSubscribedErrorCount)
    field(:sync_token, :string, source: :syncToken)
    field(:synced_at, Calcom.PrismaDateTime, source: :syncedAt)
    field(:sync_error_at, Calcom.PrismaDateTime, source: :syncErrorAt)
    field(:sync_error_count, :integer, default: 0, source: :syncErrorCount)
    field(:error, :string)
    field(:last_error_at, Calcom.PrismaDateTime, source: :lastErrorAt)
    field(:watch_attempts, :integer, default: 0, source: :watchAttempts)
    field(:unwatch_attempts, :integer, default: 0, source: :unwatchAttempts)
    field(:max_attempts, :integer, default: 3, source: :maxAttempts)
    field(:user_id, :id, source: :userId)
    field(:credential_id, :id, source: :credentialId)
    field(:delegation_credential_id, :binary_id, source: :delegationCredentialId)
    field(:event_type_id, :id, source: :eventTypeId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false, type: :id)
    belongs_to(:credential, Calcom.Credential, foreign_key: :credential_id, define_field: false, type: :id)
    belongs_to(:delegation_credential, Calcom.DelegationCredential, foreign_key: :delegation_credential_id, define_field: false)
    belongs_to(:event_type, Calcom.EventType, foreign_key: :event_type_id, define_field: false, type: :id)
    has_many(:calendar_cache_events, Calcom.CalendarCacheEvent, foreign_key: :selected_calendar_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
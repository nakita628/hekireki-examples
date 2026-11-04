defmodule Calcom.Webhook do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          subscriber_url: String.t(),
          payload_template: String.t() | nil,
          active: boolean(),
          event_triggers: atom(),
          secret: String.t() | nil,
          platform: boolean(),
          time: integer() | nil,
          time_unit: atom() | nil,
          version: String.t(),
          inserted_at: DateTime.t(),
          user: Calcom.User.t() | nil,
          team: Calcom.Team.t() | nil,
          event_type: Calcom.EventType.t() | nil,
          platform_oauth_client: Calcom.PlatformOAuthClient.t() | nil,
          app: Calcom.App.t() | nil,
          scheduled_triggers: [Calcom.WebhookScheduledTriggers.t()]
        }

  schema "Webhook" do
    field(:id, :string, primary_key: true)
    field(:subscriber_url, :string, source: :subscriberUrl)
    field(:payload_template, :string, source: :payloadTemplate)
    field(:active, :boolean, default: true)
    field(:event_triggers, {:array, Ecto.Enum}, values: [:BOOKING_CREATED, :BOOKING_PAYMENT_INITIATED, :BOOKING_PAID, :BOOKING_RESCHEDULED, :BOOKING_REQUESTED, :BOOKING_CANCELLED, :BOOKING_REJECTED, :BOOKING_NO_SHOW_UPDATED, :FORM_SUBMITTED, :MEETING_ENDED, :MEETING_STARTED, :RECORDING_READY, :RECORDING_TRANSCRIPTION_GENERATED, :OOO_CREATED, :AFTER_HOSTS_CAL_VIDEO_NO_SHOW, :AFTER_GUESTS_CAL_VIDEO_NO_SHOW, :FORM_SUBMITTED_NO_EVENT, :DELEGATION_CREDENTIAL_ERROR, :WRONG_ASSIGNMENT_REPORT], source: :eventTriggers)
    field(:secret, :string)
    field(:platform, :boolean, default: false)
    field(:time, :integer)
    field(:time_unit, Ecto.Enum, values: [DAY: "day", HOUR: "hour", MINUTE: "minute"], source: :timeUnit)
    field(:version, :string, default: "2021-10-20")
    field(:user_id, :id, source: :userId)
    field(:team_id, :id, source: :teamId)
    field(:event_type_id, :id, source: :eventTypeId)
    field(:platform_oauth_client_id, :string, source: :platformOAuthClientId)
    field(:app_id, :string, source: :appId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false)
    belongs_to(:event_type, Calcom.EventType, foreign_key: :event_type_id, define_field: false)
    belongs_to(:platform_oauth_client, Calcom.PlatformOAuthClient, foreign_key: :platform_oauth_client_id, define_field: false, type: :string)
    belongs_to(:app, Calcom.App, foreign_key: :app_id, define_field: false, type: :string, references: :slug)
    has_many(:scheduled_triggers, Calcom.WebhookScheduledTriggers, foreign_key: :webhook_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end
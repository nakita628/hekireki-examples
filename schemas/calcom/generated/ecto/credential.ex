defmodule Calcom.Credential do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          type: String.t(),
          key: map(),
          encrypted_key: String.t() | nil,
          subscription_id: String.t() | nil,
          payment_status: String.t() | nil,
          billing_cycle_start: integer() | nil,
          invalid: boolean() | nil,
          user: Calcom.User.t() | nil,
          team: Calcom.Team.t() | nil,
          app: Calcom.App.t() | nil,
          delegation_credential: Calcom.DelegationCredential.t() | nil,
          destination_calendars: [Calcom.DestinationCalendar.t()],
          selected_calendars: [Calcom.SelectedCalendar.t()],
          calendar_cache: [Calcom.CalendarCache.t()],
          references: [Calcom.BookingReference.t()],
          integration_attribute_syncs: [Calcom.IntegrationAttributeSync.t()],
          host_locations: [Calcom.HostLocation.t()]
        }

  schema "Credential" do
    field(:type, :string)
    field(:key, :map)
    field(:encrypted_key, :string, source: :encryptedKey)
    field(:subscription_id, :string, source: :subscriptionId)
    field(:payment_status, :string, source: :paymentStatus)
    field(:billing_cycle_start, :integer, source: :billingCycleStart)
    field(:invalid, :boolean, default: false)
    field(:user_id, :id, source: :userId)
    field(:team_id, :id, source: :teamId)
    field(:app_id, :string, source: :appId)
    field(:delegation_credential_id, :binary_id, source: :delegationCredentialId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false)
    belongs_to(:app, Calcom.App, foreign_key: :app_id, define_field: false, type: :string, references: :slug)
    belongs_to(:delegation_credential, Calcom.DelegationCredential, foreign_key: :delegation_credential_id, define_field: false, type: :binary_id)
    has_many(:destination_calendars, Calcom.DestinationCalendar, foreign_key: :credential_id)
    has_many(:selected_calendars, Calcom.SelectedCalendar, foreign_key: :credential_id)
    has_many(:calendar_cache, Calcom.CalendarCache, foreign_key: :credential_id)
    has_many(:references, Calcom.BookingReference, foreign_key: :credential_id)
    has_many(:integration_attribute_syncs, Calcom.IntegrationAttributeSync, foreign_key: :credential_id)
    has_many(:host_locations, Calcom.HostLocation, foreign_key: :credential_id)
  end
end
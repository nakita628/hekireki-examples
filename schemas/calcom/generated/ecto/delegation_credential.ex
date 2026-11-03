defmodule Calcom.DelegationCredential do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          service_account_key: map(),
          enabled: boolean(),
          last_enabled_at: DateTime.t() | nil,
          last_disabled_at: DateTime.t() | nil,
          domain: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace_platform: Calcom.WorkspacePlatform.t() | nil,
          organization: Calcom.Team.t() | nil,
          selected_calendars: [Calcom.SelectedCalendar.t()],
          destination_calendar: [Calcom.DestinationCalendar.t()],
          booking_references: [Calcom.BookingReference.t()],
          credentials: [Calcom.Credential.t()]
        }

  schema "DelegationCredential" do
    field(:service_account_key, :map, source: :serviceAccountKey)
    field(:enabled, :boolean, default: false)
    field(:last_enabled_at, Calcom.PrismaDateTime, source: :lastEnabledAt)
    field(:last_disabled_at, Calcom.PrismaDateTime, source: :lastDisabledAt)
    field(:domain, :string)
    field(:workspace_platform_id, :id, source: :workspacePlatformId)
    field(:organization_id, :id, source: :organizationId)
    belongs_to(:workspace_platform, Calcom.WorkspacePlatform, foreign_key: :workspace_platform_id, define_field: false, type: :id)
    belongs_to(:organization, Calcom.Team, foreign_key: :organization_id, define_field: false, type: :id)
    has_many(:selected_calendars, Calcom.SelectedCalendar, foreign_key: :delegation_credential_id)
    has_many(:destination_calendar, Calcom.DestinationCalendar, foreign_key: :delegation_credential_id)
    has_many(:booking_references, Calcom.BookingReference, foreign_key: :delegation_credential_id)
    has_many(:credentials, Calcom.Credential, foreign_key: :delegation_credential_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
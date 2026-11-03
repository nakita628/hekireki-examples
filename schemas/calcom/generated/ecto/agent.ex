defmodule Calcom.Agent do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t(),
          provider_agent_id: String.t(),
          inbound_event_type_id: integer() | nil,
          outbound_event_type_id: integer() | nil,
          enabled: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Calcom.User.t() | nil,
          team: Calcom.Team.t() | nil,
          inbound_phone_numbers: [Calcom.CalAiPhoneNumber.t()],
          outbound_phone_numbers: [Calcom.CalAiPhoneNumber.t()]
        }

  schema "Agent" do
    field(:name, :string)
    field(:provider_agent_id, :string, source: :providerAgentId)
    field(:inbound_event_type_id, :integer, source: :inboundEventTypeId)
    field(:outbound_event_type_id, :integer, source: :outboundEventTypeId)
    field(:enabled, :boolean, default: true)
    field(:user_id, :id, source: :userId)
    field(:team_id, :id, source: :teamId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false, type: :id)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false, type: :id)
    has_many(:inbound_phone_numbers, Calcom.CalAiPhoneNumber, foreign_key: :inbound_agent_id)
    has_many(:outbound_phone_numbers, Calcom.CalAiPhoneNumber, foreign_key: :outbound_agent_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
defmodule Calcom.CalAiPhoneNumber do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          phone_number: String.t(),
          provider: String.t(),
          provider_phone_number_id: String.t() | nil,
          stripe_customer_id: String.t() | nil,
          stripe_subscription_id: String.t() | nil,
          subscription_status: atom() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Calcom.User.t() | nil,
          team: Calcom.Team.t() | nil,
          inbound_agent: Calcom.Agent.t() | nil,
          outbound_agent: Calcom.Agent.t() | nil
        }

  schema "CalAiPhoneNumber" do
    field(:phone_number, :string, source: :phoneNumber)
    field(:provider, :string)
    field(:provider_phone_number_id, :string, source: :providerPhoneNumberId)
    field(:stripe_customer_id, :string, source: :stripeCustomerId)
    field(:stripe_subscription_id, :string, source: :stripeSubscriptionId)
    field(:subscription_status, Ecto.Enum, values: [:ACTIVE, :PAST_DUE, :CANCELLED, :INCOMPLETE, :INCOMPLETE_EXPIRED, :TRIALING, :UNPAID], source: :subscriptionStatus)
    field(:user_id, :id, source: :userId)
    field(:team_id, :id, source: :teamId)
    field(:inbound_agent_id, :binary_id, source: :inboundAgentId)
    field(:outbound_agent_id, :binary_id, source: :outboundAgentId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false)
    belongs_to(:inbound_agent, Calcom.Agent, foreign_key: :inbound_agent_id, define_field: false, type: :binary_id)
    belongs_to(:outbound_agent, Calcom.Agent, foreign_key: :outbound_agent_id, define_field: false, type: :binary_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
defmodule Trigger.RuntimeEnvironmentSession do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          ip_address: String.t(),
          disconnected_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          environment: Trigger.RuntimeEnvironment.t() | nil,
          current_environments: [Trigger.RuntimeEnvironment.t()]
        }

  schema "RuntimeEnvironmentSession" do
    field(:id, :string, primary_key: true)
    field(:ip_address, :string, source: :ipAddress)
    field(:disconnected_at, Trigger.PrismaDateTime, source: :disconnectedAt)
    field(:environment_id, :string, source: :environmentId)
    belongs_to(:environment, Trigger.RuntimeEnvironment, foreign_key: :environment_id, define_field: false, type: :string)
    has_many(:current_environments, Trigger.RuntimeEnvironment, foreign_key: :current_session_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
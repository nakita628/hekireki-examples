defmodule Trigger.SecretReference do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          key: String.t(),
          provider: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          environment_variable_values: [Trigger.EnvironmentVariableValue.t()],
          organization_integration: [Trigger.OrganizationIntegration.t()],
          user: [Trigger.User.t()]
        }

  schema "SecretReference" do
    field(:id, :string, primary_key: true)
    field(:key, :string)
    field(:provider, Ecto.Enum, values: [:DATABASE, :AWS_PARAM_STORE], default: :DATABASE)
    has_many(:environment_variable_values, Trigger.EnvironmentVariableValue, foreign_key: :value_reference_id, references: :id)
    has_many(:organization_integration, Trigger.OrganizationIntegration, foreign_key: :token_reference_id, references: :id)
    has_many(:user, Trigger.User, foreign_key: :mfa_secret_reference_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
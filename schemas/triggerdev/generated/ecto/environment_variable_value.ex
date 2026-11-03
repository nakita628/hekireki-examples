defmodule Trigger.EnvironmentVariableValue do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          is_secret: boolean(),
          version: integer(),
          last_updated_by: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          value_reference: Trigger.SecretReference.t() | nil,
          variable: Trigger.EnvironmentVariable.t() | nil,
          environment: Trigger.RuntimeEnvironment.t() | nil
        }

  schema "EnvironmentVariableValue" do
    field(:id, :string, primary_key: true)
    field(:is_secret, :boolean, default: false, source: :isSecret)
    field(:version, :integer, default: 1)
    field(:last_updated_by, :map, source: :lastUpdatedBy)
    field(:value_reference_id, :string, source: :valueReferenceId)
    field(:variable_id, :string, source: :variableId)
    field(:environment_id, :string, source: :environmentId)
    belongs_to(:value_reference, Trigger.SecretReference, foreign_key: :value_reference_id, define_field: false, type: :string)
    belongs_to(:variable, Trigger.EnvironmentVariable, foreign_key: :variable_id, define_field: false, type: :string)
    belongs_to(:environment, Trigger.RuntimeEnvironment, foreign_key: :environment_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
defmodule Trigger.TrustedDeployEnvironment do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          enabled: boolean(),
          scope: atom(),
          scope_value: String.t() | nil,
          additional_claims: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          trusted_deploy_repository: Trigger.TrustedDeployRepository.t() | nil,
          runtime_environment: Trigger.RuntimeEnvironment.t() | nil
        }

  schema "TrustedDeployEnvironment" do
    field(:id, :string, primary_key: true)
    field(:enabled, :boolean, default: false)
    field(:scope, Ecto.Enum, values: [:ANY, :BRANCH, :TAG, :ENVIRONMENT])
    field(:scope_value, :string, source: :scopeValue)
    field(:additional_claims, :map, default: %{}, source: :additionalClaims)
    field(:trusted_deploy_repository_id, :string, source: :trustedDeployRepositoryId)
    field(:runtime_environment_id, :string, source: :runtimeEnvironmentId)
    belongs_to(:trusted_deploy_repository, Trigger.TrustedDeployRepository, foreign_key: :trusted_deploy_repository_id, define_field: false, type: :string)
    belongs_to(:runtime_environment, Trigger.RuntimeEnvironment, foreign_key: :runtime_environment_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
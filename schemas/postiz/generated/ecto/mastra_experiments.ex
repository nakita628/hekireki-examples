defmodule Postiz.MastraExperiments do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t() | nil,
          description: String.t() | nil,
          metadata: map() | nil,
          dataset_id: String.t() | nil,
          dataset_version: integer() | nil,
          target_type: String.t(),
          target_id: String.t(),
          status: String.t(),
          total_items: integer(),
          succeeded_count: integer(),
          failed_count: integer(),
          skipped_count: integer(),
          started_at: DateTime.t() | nil,
          completed_at: DateTime.t() | nil,
          agent_version: String.t() | nil,
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          started_at_z: DateTime.t() | nil,
          completed_at_z: DateTime.t() | nil,
          created_at_z: DateTime.t() | nil,
          updated_at_z: DateTime.t() | nil,
          organization_id: String.t() | nil,
          project_id: String.t() | nil,
          provenance: map() | nil,
          runner_attestation: map() | nil,
          experiment_set_id: String.t() | nil,
          comparison_id: String.t() | nil,
          variant_id: String.t() | nil,
          trial_index: integer() | nil,
          scorer_ids: map() | nil
        }

  schema "mastra_experiments" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:metadata, :map)
    field(:dataset_id, :string, source: :datasetId)
    field(:dataset_version, :integer, source: :datasetVersion)
    field(:target_type, :string, source: :targetType)
    field(:target_id, :string, source: :targetId)
    field(:status, :string)
    field(:total_items, :integer, source: :totalItems)
    field(:succeeded_count, :integer, source: :succeededCount)
    field(:failed_count, :integer, source: :failedCount)
    field(:skipped_count, :integer, source: :skippedCount)
    field(:started_at, Postiz.PrismaDateTime, source: :startedAt)
    field(:completed_at, Postiz.PrismaDateTime, source: :completedAt)
    field(:agent_version, :string, source: :agentVersion)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:updated_at, Postiz.PrismaDateTime, source: :updatedAt)
    field(:started_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :startedAtZ)
    field(:completed_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :completedAtZ)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
    field(:updated_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :updatedAtZ)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    field(:provenance, :map)
    field(:runner_attestation, :map, source: :runnerAttestation)
    field(:experiment_set_id, :string, source: :experimentSetId)
    field(:comparison_id, :string, source: :comparisonId)
    field(:variant_id, :string, source: :variantId)
    field(:trial_index, :integer, source: :trialIndex)
    field(:scorer_ids, :map, source: :scorerIds)
  end
end
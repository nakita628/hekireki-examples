defmodule Trigger.ErrorGroupState do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          task_identifier: String.t(),
          error_fingerprint: String.t(),
          status: atom(),
          ignored_until: DateTime.t() | nil,
          ignored_until_occurrence_rate: integer() | nil,
          ignored_until_total_occurrences: integer() | nil,
          ignored_at_occurrence_count: integer() | nil,
          ignored_at: DateTime.t() | nil,
          ignored_reason: String.t() | nil,
          ignored_by_user_id: String.t() | nil,
          resolved_at: DateTime.t() | nil,
          resolved_in_version: String.t() | nil,
          resolved_by: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Trigger.Organization.t() | nil,
          project: Trigger.Project.t() | nil,
          environment: Trigger.RuntimeEnvironment.t() | nil
        }

  schema "ErrorGroupState" do
    field(:id, :string, primary_key: true)
    field(:task_identifier, :string, source: :taskIdentifier)
    field(:error_fingerprint, :string, source: :errorFingerprint)
    field(:status, Ecto.Enum, values: [:UNRESOLVED, :RESOLVED, :IGNORED], default: :UNRESOLVED)
    field(:ignored_until, Trigger.PrismaDateTime, source: :ignoredUntil)
    field(:ignored_until_occurrence_rate, :integer, source: :ignoredUntilOccurrenceRate)
    field(:ignored_until_total_occurrences, :integer, source: :ignoredUntilTotalOccurrences)
    field(:ignored_at_occurrence_count, :integer, source: :ignoredAtOccurrenceCount)
    field(:ignored_at, Trigger.PrismaDateTime, source: :ignoredAt)
    field(:ignored_reason, :string, source: :ignoredReason)
    field(:ignored_by_user_id, :string, source: :ignoredByUserId)
    field(:resolved_at, Trigger.PrismaDateTime, source: :resolvedAt)
    field(:resolved_in_version, :string, source: :resolvedInVersion)
    field(:resolved_by, :string, source: :resolvedBy)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    field(:environment_id, :string, source: :environmentId)
    belongs_to(:organization, Trigger.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:environment, Trigger.RuntimeEnvironment, foreign_key: :environment_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
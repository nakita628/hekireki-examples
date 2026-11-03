defmodule Formbricks.RetentionPolicy do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          entity: atom(),
          enabled: boolean(),
          enabled_at: DateTime.t() | nil,
          warn_days: integer(),
          period_days: integer(),
          conditions: atom(),
          deferred_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Formbricks.Organization.t() | nil,
          updated_by: Formbricks.User.t() | nil
        }

  schema "RetentionPolicy" do
    field(:id, :string, primary_key: true)
    field(:entity, Ecto.Enum, values: [:responses, :surveys, :members])
    field(:enabled, :boolean, default: false)
    field(:enabled_at, Formbricks.PrismaDateTime, source: :enabledAt)
    field(:warn_days, :integer, default: 60, source: :warnDays)
    field(:period_days, :integer, source: :periodDays)
    field(:conditions, {:array, Ecto.Enum}, values: [:noResponse, :noChange, :createdBefore])
    field(:deferred_at, Formbricks.PrismaDateTime, source: :deferredAt)
    field(:organization_id, :string, source: :organizationId)
    field(:updated_by_id, :string, source: :updatedById)
    belongs_to(:organization, Formbricks.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:updated_by, Formbricks.User, foreign_key: :updated_by_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end
defmodule Formbricks.RetentionExemption do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          entity: atom(),
          until: DateTime.t(),
          source: atom(),
          reason: String.t() | nil,
          revoked_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          organization: Formbricks.Organization.t() | nil,
          survey: Formbricks.Survey.t() | nil,
          created_by: Formbricks.User.t() | nil,
          revoked_by: Formbricks.User.t() | nil
        }

  schema "RetentionExemption" do
    field(:id, :string, primary_key: true)
    field(:entity, Ecto.Enum, values: [:responses, :surveys, :members])
    field(:until, Formbricks.PrismaDateTime)
    field(:source, Ecto.Enum, values: [:manual, :restore], default: :manual)
    field(:reason, :string)
    field(:revoked_at, Formbricks.PrismaDateTime, source: :revokedAt)
    field(:organization_id, :string, source: :organizationId)
    field(:survey_id, :string, source: :surveyId)
    field(:created_by_id, :string, source: :createdById)
    field(:revoked_by_id, :string, source: :revokedById)
    belongs_to(:organization, Formbricks.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:survey, Formbricks.Survey, foreign_key: :survey_id, define_field: false, type: :string)
    belongs_to(:created_by, Formbricks.User, foreign_key: :created_by_id, define_field: false, type: :string)
    belongs_to(:revoked_by, Formbricks.User, foreign_key: :revoked_by_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end
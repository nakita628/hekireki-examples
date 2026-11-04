defmodule Formbricks.FeedbackDirectory do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          is_archived: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Formbricks.Organization.t() | nil,
          workspaces: [Formbricks.FeedbackDirectoryWorkspace.t()],
          feedback_sources: [Formbricks.FeedbackSource.t()],
          charts: [Formbricks.Chart.t()]
        }

  schema "FeedbackDirectory" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:is_archived, :boolean, default: false, source: :isArchived)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, Formbricks.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    has_many(:workspaces, Formbricks.FeedbackDirectoryWorkspace, foreign_key: :feedback_directory_id, references: :id)
    has_many(:feedback_sources, Formbricks.FeedbackSource, foreign_key: :feedback_directory_id, references: :id)
    has_many(:charts, Formbricks.Chart, foreign_key: :feedback_directory_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end
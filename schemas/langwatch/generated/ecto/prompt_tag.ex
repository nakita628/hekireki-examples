defmodule LangWatch.PromptTag do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil,
          created_by: LangWatch.User.t() | nil,
          updated_by: LangWatch.User.t() | nil,
          assignments: [LangWatch.PromptTagAssignment.t()]
        }

  schema "PromptTag" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:organization_id, :string, source: :organizationId)
    field(:created_by_id, :string, source: :createdById)
    field(:updated_by_id, :string, source: :updatedById)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:created_by, LangWatch.User, foreign_key: :created_by_id, define_field: false, type: :string)
    belongs_to(:updated_by, LangWatch.User, foreign_key: :updated_by_id, define_field: false, type: :string)
    has_many(:assignments, LangWatch.PromptTagAssignment, foreign_key: :tag_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
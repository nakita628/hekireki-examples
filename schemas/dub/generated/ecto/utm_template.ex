defmodule Dub.UtmTemplate do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          utm_source: String.t() | nil,
          utm_medium: String.t() | nil,
          utm_campaign: String.t() | nil,
          utm_term: String.t() | nil,
          utm_content: String.t() | nil,
          ref: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Dub.User.t() | nil,
          project: Dub.Project.t() | nil
        }

  schema "UtmTemplate" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:utm_source, :string)
    field(:utm_medium, :string)
    field(:utm_campaign, :string)
    field(:utm_term, :string)
    field(:utm_content, :string)
    field(:ref, :string)
    field(:user_id, :string, source: :userId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:user, Dub.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:project, Dub.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
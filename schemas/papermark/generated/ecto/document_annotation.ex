defmodule Papermark.DocumentAnnotation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          title: String.t(),
          content: map(),
          pages: [integer()],
          is_visible: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          document: Papermark.Document.t() | nil,
          team: Papermark.Team.t() | nil,
          created_by: Papermark.User.t() | nil,
          images: [Papermark.AnnotationImage.t()]
        }

  schema "DocumentAnnotation" do
    field(:id, :string, primary_key: true)
    field(:title, :string)
    field(:content, :map)
    field(:pages, {:array, :integer})
    field(:is_visible, :boolean, default: true, source: :isVisible)
    field(:document_id, :string, source: :documentId)
    field(:team_id, :string, source: :teamId)
    field(:created_by_id, :string, source: :createdById)
    belongs_to(:document, Papermark.Document, foreign_key: :document_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    belongs_to(:created_by, Papermark.User, foreign_key: :created_by_id, define_field: false, type: :string)
    has_many(:images, Papermark.AnnotationImage, foreign_key: :annotation_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
defmodule Papermark.AnnotationImage do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          filename: String.t(),
          url: String.t(),
          size: integer() | nil,
          mime_type: String.t() | nil,
          inserted_at: DateTime.t(),
          annotation: Papermark.DocumentAnnotation.t() | nil
        }

  schema "AnnotationImage" do
    field(:id, :string, primary_key: true)
    field(:filename, :string)
    field(:url, :string)
    field(:size, :integer)
    field(:mime_type, :string, source: :mimeType)
    field(:annotation_id, :string, source: :annotationId)
    belongs_to(:annotation, Papermark.DocumentAnnotation, foreign_key: :annotation_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end
defmodule Postiz.ClippingClip do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          title: String.t(),
          content: String.t(),
          start: float(),
          end: float(),
          trim_start: integer() | nil,
          status: String.t(),
          error: String.t() | nil,
          media_id: String.t() | nil,
          path: String.t() | nil,
          thumbnail: String.t() | nil,
          drafted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          clipping: Postiz.Clipping.t() | nil
        }

  schema "ClippingClip" do
    field(:title, :string)
    field(:content, :string)
    field(:start, :float)
    field(:end, :float)
    field(:trim_start, :integer, source: :trimStart)
    field(:status, :string, default: "pending")
    field(:error, :string)
    field(:media_id, :string, source: :mediaId)
    field(:path, :string)
    field(:thumbnail, :string)
    field(:drafted_at, Postiz.PrismaDateTime, source: :draftedAt)
    field(:clipping_id, :binary_id, source: :clippingId)
    belongs_to(:clipping, Postiz.Clipping, foreign_key: :clipping_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end
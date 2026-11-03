defmodule Postiz.MastraSkillBlobs do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          hash: String.t(),
          content: String.t(),
          size: integer(),
          mime_type: String.t() | nil,
          created_at: DateTime.t(),
          created_at_z: DateTime.t() | nil
        }

  schema "mastra_skill_blobs" do
    field(:hash, :string, primary_key: true)
    field(:content, :string)
    field(:size, :integer)
    field(:mime_type, :string, source: :mimeType)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
  end
end
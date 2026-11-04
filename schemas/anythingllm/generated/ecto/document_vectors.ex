defmodule AnythingLLM.DocumentVectors do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          doc_id: String.t(),
          vector_id: String.t(),
          last_updated_at: DateTime.t(),
          inserted_at: DateTime.t()
        }

  schema "document_vectors" do
    field(:doc_id, :string, source: :docId)
    field(:vector_id, :string, source: :vectorId)
    field(:last_updated_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :lastUpdatedAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end
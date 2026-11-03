defmodule Postiz.MastraKnowledgeMentions do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          source_type: String.t(),
          source_id: String.t(),
          record_id: String.t()
        }

  schema "mastra_knowledge_mentions" do
    field(:source_type, :string, primary_key: true, source: :sourceType)
    field(:source_id, :string, primary_key: true, source: :sourceId)
    field(:record_id, :string, primary_key: true, source: :recordId)
  end
end
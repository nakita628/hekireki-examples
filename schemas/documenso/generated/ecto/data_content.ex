defmodule Documenso.DataContent do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          type: atom(),
          data: String.t(),
          metadata: map(),
          envelope_contents: [Documenso.EnvelopeContent.t()]
        }

  schema "DataContent" do
    field(:id, :string, primary_key: true)
    field(:type, Ecto.Enum, values: [:S3_PATH, :BYTES, :BYTES_64])
    field(:data, :string)
    field(:metadata, :map)
    has_many(:envelope_contents, Documenso.EnvelopeContent, foreign_key: :data_content_id, references: :id)
  end
end
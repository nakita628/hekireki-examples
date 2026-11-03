defmodule Documenso.DocumentData do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          type: atom(),
          data: String.t(),
          initial_data: String.t(),
          envelope_item: Documenso.EnvelopeItem.t() | nil
        }

  schema "DocumentData" do
    field(:id, :string, primary_key: true)
    field(:type, Ecto.Enum, values: [:S3_PATH, :BYTES, :BYTES_64])
    field(:data, :string)
    field(:initial_data, :string, source: :initialData)
    has_one(:envelope_item, Documenso.EnvelopeItem, foreign_key: :document_data_id)
  end
end
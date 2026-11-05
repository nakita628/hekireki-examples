defmodule Chibisafe.QuarantineFiles do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          file_id: String.t(),
          name: String.t(),
          reason: String.t() | nil,
          file: Chibisafe.Files.t() | nil
        }

  schema "quarantineFiles" do
    field(:name, :string)
    field(:reason, :string)
    field(:file_id, :string, primary_key: true, source: :fileId)
    belongs_to(:file, Chibisafe.Files, foreign_key: :file_id, define_field: false, type: :string, references: :uuid)
  end
end
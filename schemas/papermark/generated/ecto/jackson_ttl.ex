defmodule Papermark.JacksonTtl do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          key: String.t(),
          expires_at: integer()
        }

  schema "jackson_ttl" do
    field(:key, :string, primary_key: true)
    field(:expires_at, :integer, source: :expiresAt)
  end
end
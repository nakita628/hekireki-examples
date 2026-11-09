defmodule Jitsu.SourceCheck do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          package: String.t(),
          version: String.t(),
          key: String.t(),
          status: String.t(),
          description: String.t() | nil,
          timestamp: DateTime.t()
        }

  schema "source_check" do
    field(:package, :string)
    field(:version, :string)
    field(:key, :string, primary_key: true)
    field(:status, :string)
    field(:description, :string)
    field(:timestamp, Jitsu.PrismaDateTime, autogenerate: true)
  end
end
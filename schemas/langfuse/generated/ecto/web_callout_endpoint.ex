defmodule Langfuse.WebCalloutEndpoint do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          url: String.t(),
          enabled: boolean(),
          toast_message: String.t(),
          request_headers: String.t() | nil,
          request_header_keys: [String.t()],
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil
        }

  schema "web_callout_endpoints" do
    field(:id, :string, primary_key: true)
    field(:name, :string, default: "Default")
    field(:url, :string)
    field(:enabled, :boolean, default: true)
    field(:toast_message, :string, default: "Callout sent")
    field(:request_headers, :string)
    field(:request_header_keys, {:array, :string})
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end